using Abasto.Library.DevExtreme.Config;
using Abasto.Library.General;
using Abasto.Library.Interfaces;
using Abasto.Library.Property;
using Abasto.Library.Property.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Library.DevExtreme
{
    public static class PaginateTask
    {
        public static async Task<IPaginateResult<T>> Paginate<T>(this IQueryable<T> source, string filter, bool async, Action<QueryFilter> options) where T : class
        {
            QueryFilter queryFilter = new QueryFilter();
            options?.Invoke(queryFilter);

            FilterClient filterClient = new FilterClient();
            IQueryable query = source.AsQueryable();
            PropertyDescriptorCollection property = TypeDescriptor.GetProperties(typeof(T));
            if (!string.IsNullOrEmpty(filter))
            {
                filterClient = JsonConvert.DeserializeObject<FilterClient>(filter);
                query = query.QueryWhere(filterClient, property);
                if (filterClient.isLoadingAll == true)
                {
                    async = false; // desactivar async para que busque los datos estatico
                    query = (async ? await ((IQueryable<T>)query).ToListAsync()
                        : ((IQueryable<T>)query).ToList()).AsQueryable();
                }
            }
            if (async) return await query.PaginateResult<T>(filterClient, queryFilter, property, async);
            return query.PaginateResult<T>(filterClient, queryFilter, property, async).GetAwaiter().GetResult();
        }
        private static async Task<IPaginateResult<T>> PaginateResult<T>(this IQueryable source, FilterClient filterClient, QueryFilter queryFilter, PropertyDescriptorCollection property, bool async)
        {
            PaginateResult<T> paginateResult = new PaginateResult<T>();
            paginateResult.totalCount = async ? await source.QueryCountAsync(filterClient, async) : source.QueryCountAsync(filterClient, async).GetAwaiter().GetResult();
            paginateResult.summary = async ? await source.QuerySummaryAsync(filterClient, async, paginateResult.totalCount) : source.QuerySummaryAsync(filterClient, async, paginateResult.totalCount).GetAwaiter().GetResult();
            source = source.QueryOrderBy(filterClient, queryFilter, property);
            IQueryable query = source.QueryGroupBy(filterClient, queryFilter, property);
            paginateResult.groupCount = async ? await query.QueryGroupCountAsync(filterClient, async, property) : query.QueryGroupCountAsync(filterClient, async, property).GetAwaiter().GetResult();
            query = query.QuerySkipTake(filterClient);
            if (filterClient.group != null)
            {
                paginateResult.groupData = async ? await query.ToListAsync() : ((IQueryable<object>)query).ToList();
                if (string.IsNullOrEmpty(filterClient.dataField)) paginateResult.groupData = ToListGroupBy<T>(paginateResult.groupData, filterClient, queryFilter);
                return paginateResult;
            }
            if (async) paginateResult.data = await ((IQueryable<T>)query).ToListAsync();
            else paginateResult.data = ((IQueryable<T>)query).ToList();
            return paginateResult;
        }
        private static IQueryable QueryWhere(this IQueryable query, FilterClient filterClient, PropertyDescriptorCollection property)
        {
            if (filterClient.searchExpr != null
                && filterClient.searchOperation != null
                && filterClient.searchValue != null)
            {
                FilterSearch(filterClient);
            }
            if (filterClient.filter == null) return query;
            List<object> param = new List<object>();
            string consulta = filterClient.filter.ConsultaWhere(property, ref param);
            if (!string.IsNullOrEmpty(consulta)) query = query.Where(consulta, param.ToArray());
            return query;
        }
        private static void FilterSearch(FilterClient filterClient)
        {
            if (typeof(string) == filterClient.searchExpr.GetType())
            {
                if (filterClient.searchExpr.ToString().Contains(","))
                {
                    filterClient.searchExpr = filterClient.searchExpr.ToString().Split(',');
                    FilterSearch(filterClient);
                    return;
                }

                var filters = new List<object>();

                var filter = new List<object>();
                filter.Add(filterClient.searchExpr);
                filter.Add(filterClient.searchOperation);
                filter.Add(filterClient.searchValue);

                if (filterClient.filter != null)
                {
                    filters = filterClient.filter.ToList();
                    filters.Add(filter.ToArray());

                    var filtros = filterClient.filter.ToList();
                    filtros.Add("and");
                    filtros.Add(filters.ToArray());
                    filterClient.filter = filtros.ToArray();
                }
                else
                {
                    filters.AddRange(filter.ToArray());
                    filterClient.filter = filters.ToArray();
                }
            }
            else
            {
                var searchExpr = new List<object>((IEnumerable<object>)filterClient.searchExpr);
                var filters = new List<object>();
                int i = 0;
                foreach (var item in searchExpr)
                {
                    if (i > 0)
                    {
                        filters.Add("or");
                    }
                    var filter = new List<object>();
                    filter.Add(item);
                    filter.Add(filterClient.searchOperation);
                    filter.Add(filterClient.searchValue);
                    filters.Add(filter.ToArray());
                    i++;
                }
                if (filterClient.filter != null)
                {
                    var filter = filterClient.filter.ToList();
                    filter.Add("and");
                    filter.Add(filters.ToArray());
                    filterClient.filter = filter.ToArray();
                }
                else
                {
                    filterClient.filter = filters.ToArray();
                }
            }
        }
        private static async Task<List<object>> QuerySummaryAsync(this IQueryable query, FilterClient filterClient, bool async, int? totalCount)
        {
            if (filterClient.totalSummary == null) return null;

            List<SummaryItem> summaryItem = filterClient.totalSummary.Select((x, index) => new SummaryItem
            {
                orden = index,
                selector = x.selector,
                summaryType = x.summaryType,
                value = (object)null

            }).ToList();
            var existe = new List<(string, object)>();
            foreach (var item in summaryItem)
            {
                object total = null;
                if (totalCount != 0)
                {

                    try
                    {
                        if (item.summaryType == "count")
                        {
                            if (totalCount != null) total = totalCount;
                            else
                            {
                                total = async ? await query.QueryCount(async) : query.QueryCount(async).GetAwaiter().GetResult();
                                totalCount = (int?)total;
                            }
                        }
                        else if (!string.IsNullOrEmpty(item.selector) && !string.IsNullOrEmpty(item.summaryType))
                        {
                            if (item.summaryType == "sum") total = query.Aggregate("Sum", item.selector);
                            else if (item.summaryType == "avg") total = query.Aggregate("Average", item.selector);
                            else if (item.summaryType == "min") total = query.Aggregate("Min", item.selector);
                            else if (item.summaryType == "max") total = query.Aggregate("Max", item.selector);
                        }
                        else continue;
                    }
                    catch { total = 0; }
                }

                item.value = total;
            }
            if (summaryItem.Count() == 0) return null;
            return summaryItem.OrderBy(x => x.orden).Select(x => x.value).ToList();
        }
        private static IQueryable QueryOrderBy(this IQueryable source, FilterClient filterClient, QueryFilter queryFilter, PropertyDescriptorCollection property)
        {
            if (filterClient.group != null) return source;
            else if (filterClient.sort == null && filterClient.key == null && string.IsNullOrEmpty(queryFilter.orderBy)) return source;
            else if (filterClient.sort == null && filterClient.key != null && string.IsNullOrEmpty(queryFilter.orderBy))
            {
                filterClient.sort = new List<SortFilter>();
                foreach (string item in filterClient.key)
                {
                    var sort = new SortFilter();
                    sort.selector = item;
                    var propertyType = property.GetProperty(item);
                    if (typeof(string) == propertyType) sort.desc = false;
                    else sort.desc = true;
                    filterClient.sort.Add(sort);
                }
            }
            IOrderedQueryable query = (IOrderedQueryable)source.AsQueryable();
            bool orderBy = true;
            if (filterClient.sort != null)
            {
                foreach (var item in filterClient.sort)
                {
                    string order = item.selector;
                    if (item.selector.Contains(",")) order = order.ReplaceAll(",", $"{(item.desc ? " desc @" : " asc @")}").ReplaceAll("@", ",");
                    else order += item.desc ? " desc" : " asc";
                    if (orderBy) query = query.OrderBy(order);
                    else query = query.ThenBy(order);
                    orderBy = false;
                }
            }
            if (!string.IsNullOrEmpty(queryFilter.orderBy))
            {
                if (orderBy) query = query.OrderBy(queryFilter.orderBy);
                else query = query.ThenBy(queryFilter.orderBy);
            }
            return query.AsQueryable();
        }
        private static IQueryable QueryGroupBy(this IQueryable source, FilterClient filterClient, QueryFilter queryFilter, PropertyDescriptorCollection property)
        {
            if (filterClient.group == null) return source;
            string consulta = string.Empty, key = string.Empty, order = string.Empty;
            List<object> groupObject = new List<object>();
            var listaGroup = new List<string>();
            int count = filterClient.groupSummary != null ? 0 : filterClient.group.Count() - 1,
                ini = 1;
            foreach (var item in filterClient.group)
            {
                bool keyMultiple = item.selector.Contains(",");
                string groupBy = item.selector;
                if (!keyMultiple)
                {
                    if (item.groupInterval != null)
                    {
                        Type groupIntervalType = item.groupInterval.GetType();
                        if (groupIntervalType == typeof(string))
                        {
                            if ("year,month,day".Contains(item.groupInterval.ToString()))
                            {
                                groupBy = $"SqlFunctions.DatePart(\"{item.groupInterval}\",{item.selector})";
                            }
                        }
                        else if (groupIntervalType.GetTypeIsNumeric())
                        {
                            int c = groupObject.Count();
                            object value = property.GetConvertValueTruncate(item.selector, item.groupInterval);
                            groupObject.Add(value);
                            groupBy = $"DbFunctions.Truncate((({item.selector})/@{c}),0)*@{c}";
                        }
                    }
                }
                if (string.IsNullOrEmpty(consulta)) consulta = "it.Key as key";
                else if (!keyMultiple) consulta += $", it.GroupBy({groupBy}).Select(new (it.Key as key";
                else consulta += $", it.GroupBy(new ({item.selector})).Select(new (it.Key as key";

                if (string.IsNullOrEmpty(filterClient.dataField) && count < ini) consulta += $",it.Count() as count";
                if (filterClient.groupSummary != null)
                {
                    var summary = new List<string>();
                    foreach (var grup in filterClient.groupSummary)
                    {
                        if (grup.summaryType == "count") continue;
                        else if (string.IsNullOrEmpty(grup.selector)) continue;
                        else if (summary.Where(x => x == (grup.selector + grup.summaryType)).Any()) continue;
                        else if (grup.summaryType == "sum") consulta += $",it.Sum({grup.selector})";
                        else if (grup.summaryType == "avg") consulta += $",it.Average({grup.selector})";
                        else if (grup.summaryType == "max") consulta += $",it.Min({grup.selector})";
                        else if (grup.summaryType == "min") consulta += $",it.Max({grup.selector})";
                        else continue;
                        summary.Add($"{grup.selector}{grup.summaryType}");
                        consulta += $" as {grup.selector}{grup.summaryType}";
                    }
                }

                if (string.IsNullOrEmpty(key))
                {
                    key = groupBy;
                    order = item.desc != true ? "asc" : "desc";
                }
                ini++;
            }
            if (filterClient.isLoadingAll == true) consulta += ",it.ToList() as data";
            int contar = filterClient.group.Count();
            for (int i = 1; i < contar; i++) consulta += ")).OrderBy(key).ToList() as items";

            if (!key.Contains(")") && key.Contains(","))
            {
                var keySplit = key.Split(',');
                key = "new (" + key + ")";
                var orderBy = order;
                order = string.Empty;
                foreach (var k in keySplit) order += "key." + k + " " + orderBy + ",";
                order = order.Trim(',');
            }
            else order = "key " + order;
            IQueryable query = source.GroupBy(key, groupObject.ToArray()).Select($"new({consulta})").OrderBy(order);
            return query;
        }
        private static List<object> ToListGroupBy<T>(List<object> data, FilterClient filterClient, QueryFilter queryFilter)
        {
            if (data == null) return null;
            var group = new List<object>();
            List<SummaryItem> summaryDefault = new List<SummaryItem>();
            if (filterClient.groupSummary != null)
            {
                summaryDefault = filterClient.groupSummary.Select((x, i) => new SummaryItem
                {
                    orden = i,
                    selector = x.selector,
                    summaryType = x.summaryType,
                    value = (object)null,
                }).ToList();
            }

            foreach (var item in data)
            {
                var row = new PaginateGroup();
                Type type = item.GetType();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
                List<SummaryItem> summaryItem = summaryDefault;
                foreach (PropertyDescriptor prop in properties)
                {
                    var valor = properties[prop.Name].GetValue(item);
                    if (prop.Name == "key") row.key = valor;
                    else if (prop.Name == "items")
                    {
                        var items = new List<object>((IEnumerable<object>)valor);
                        row.items = ToListGroupBy<T>(items, filterClient, queryFilter);
                    }
                    else if (prop.Name == "data")
                    {
                        if (!string.IsNullOrEmpty(queryFilter.orderBy))
                        {
                            row.items = ((IQueryable<object>)new List<T>((IEnumerable<T>)valor)
                                .AsQueryable().OrderBy(queryFilter.orderBy)).ToList();
                        }
                        else row.items = new List<object>((IEnumerable<object>)valor);
                    }
                    else if (filterClient.groupSummary != null)
                    {
                        if (prop.Name == "count") row.count = valor;
                        var summarry = summaryItem.Where(x => (x.selector + x.summaryType) == prop.Name || (x.summaryType == "count" && prop.Name == "count")).Select(x => new { x.orden }).ToList();
                        foreach (var sum in summarry)
                        {
                            summaryItem.Where(x => x.orden == sum.orden).FirstOrDefault().value = valor;
                        }
                    }
                    else if (prop.Name == "count") row.count = valor;
                }
                if (filterClient.groupSummary != null)
                {
                    row.summary = summaryItem.OrderBy(x => x.orden).Select(x => x.value).ToList();
                }
                if (row.items != null) row.count = row.items.Count();
                group.Add(row);
            }
            return group;
        }
        private static IQueryable QuerySkipTake(this IQueryable query, FilterClient filterClient)
        {
            if (filterClient.skip != null) query = query.Skip(filterClient.skip.Value);
            if (filterClient.take != null) query = query.Take(filterClient.take.Value);
            return query;
        }
        private static async Task<int?> QueryCountAsync(this IQueryable query, FilterClient filterClient, bool async)
        {
            if (filterClient.requireTotalCount != true) return null;
            return async ? await query.QueryCount(async) : query.QueryCount(async).GetAwaiter().GetResult();
        }
        private static async Task<int?> QueryGroupCountAsync(this IQueryable query, FilterClient filterClient, bool async, PropertyDescriptorCollection property)
        {
            if (filterClient.requireGroupCount != true) return null;
            return async ? await query.QueryCount(async) : query.QueryCount(async).GetAwaiter().GetResult();
        }
        private static async Task<int> QueryCount(this IQueryable query, bool async)
        {
            return async ? await ((IQueryable<object>)query.AsQueryable()).CountAsync() : ((IQueryable<object>)query.AsQueryable()).Count();
        }
        private static string ConsultaWhere(this object[] data, PropertyDescriptorCollection property, ref List<object> param)
        {
            string consulta = string.Empty;
            if (data == null) return consulta;
            string columna = string.Empty, operador = string.Empty, union = string.Empty, recursiva = string.Empty;
            object valor = null;
            int c = 0;
            foreach (object item in data)
            {
                Type type = item != null ? item.GetType() : null;
                if (type == typeof(JArray) || type == typeof(Array) || type == typeof(object[]))
                {
                    if (string.IsNullOrEmpty(union) && !string.IsNullOrEmpty(recursiva)) continue;
                    var recurso = new List<object>((IEnumerable<object>)item).ToArray().ConsultaWhere(property, ref param);
                    recursiva += (string.IsNullOrEmpty(recurso)) ? string.Empty : $" {union} ({recurso})";
                    c = 3;
                    union = string.Empty; columna = string.Empty; operador = string.Empty; valor = null;
                }
                else
                {
                    if (c == 0) columna = item.ToString();
                    else if (c == 1) operador = item.ToString();
                    else if (c == 2) valor = item != null ? item : null;
                    else if (c == 3)
                    {
                        union = string.IsNullOrEmpty(recursiva) ? string.Empty : item.ToString();
                        c = -1;
                    }
                    c++;
                }

                if (c == 3)
                {
                    if (!string.IsNullOrEmpty(columna) && !string.IsNullOrEmpty(operador))
                    {
                        if (!columna.Contains(","))
                        {
                            TypeProperty typeProperty = new TypeProperty();
                            if (columna.Contains("."))
                            {
                                var col = columna.Split('.');
                                columna = col[0];
                                typeProperty = property.GetTypeProperty(columna);
                                if (typeProperty.isNullable) columna += ".Value";
                                if (typeProperty.type == typeof(DateTime)) valor = typeof(int).GetConvertValue(valor);
                                else valor = typeProperty.type.GetConvertValue(valor);
                                columna += "." + col[1];
                            }
                            else
                            {
                                typeProperty = property.GetTypeProperty(columna);
                                if (operador == "contains")
                                {
                                    valor = typeof(string).GetConvertValue(valor);
                                    if (typeProperty.isNullable) columna += ".Value";
                                }
                                else
                                {
                                    valor = typeProperty.type.GetConvertValue(valor);
                                }
                            }
                            consulta += QueryString(columna, operador, valor, typeProperty, ref param);
                        }
                        else
                        {
                            var listColumna = columna.Split(',');
                            JObject key = (JObject)valor;
                            int ini = 0;
                            foreach (var col in listColumna)
                            {
                                columna = col;
                                var typeProperty = property.GetTypeProperty(columna);
                                object value = key[col];
                                value = typeProperty.type.GetConvertValue(value);
                                if (operador == "contains" && typeProperty.isNumeric)
                                {
                                    if (typeProperty.isNullable) columna += ".Value";
                                }
                                if (ini > 0) consulta += " and ";
                                consulta += QueryString(columna, operador, value, typeProperty, ref param);
                                columna = string.Empty;
                                ini++;
                            }
                        }
                    }
                }
            }
            consulta = $"{recursiva} {consulta}";
            return consulta.Trim();
        }
        private static string QueryString(string columna, string operador, object valor, TypeProperty type, ref List<object> param)
        {
            if (typeof(string) == type.type && valor == null) return $"string.IsNullOrEmpty({columna}) ";
            else if (valor == null) return $"{columna} == null ";
            int pos = param.Count();
            if (operador == "=") operador = $" == @{pos}";
            else if (operador == ">" || operador == ">=" || operador == "<" || operador == "<=") operador = $" {operador} @{pos}";
            else if (operador == "contains")
            {
                if (type.type == typeof(string)) operador = $".ToLower().Contains(@{pos}.ToLower())";
                else operador = $".ToString().Contains(@{pos})";
            }
            param.Add(valor);
            return $"{columna}{operador} ";
        }
    }
}
