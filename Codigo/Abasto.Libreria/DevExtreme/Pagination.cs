using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using EntityFramework.DynamicLinq;

namespace Abasto.Libreria.DevExtreme
{
    public static partial class Pagination
    {
        public static async Task<object> PaginateAsync<T>(this IQueryable<T> source, string filter) where T : class
        {
            return await source.PaginateAsync(filter, true);
        }
        public static object Paginate<T>(this IQueryable<T> source, string filter) where T : class
        {
            return source.PaginateAsync(filter, false).GetAwaiter().GetResult();
        }
        private static async Task<object> PaginateAsync<T>(this IQueryable<T> source, string filtro, bool async) where T : class
        {
            object totalCount = null, groupCount = null;
            List<object> summary = null;
            IQueryable query = source.AsQueryable();
            if (!string.IsNullOrEmpty(filtro))
            {
                var paginate = JsonConvert.DeserializeObject<FiltroPaginate>(filtro);
                query = query.QueryWhere(paginate);
                summary = async ? await query.QuerySummaryAsync(paginate, async) : query.QuerySummaryAsync(paginate, async).GetAwaiter().GetResult();
                query = query.QueryOrderBy(paginate);
                totalCount = async ? await query.QueryCountAsync(paginate, async) : query.QueryCountAsync(paginate, async).GetAwaiter().GetResult();
                query = query.QueryGroupBy(paginate);
                groupCount = async ? await query.QueryGroupCountAsync(paginate, async) : query.QueryGroupCountAsync(paginate, async).GetAwaiter().GetResult();
                query = query.QuerySkipTake(paginate);
            }
            var obj = new
            {
                data = async ? await query.ToListAsync() : query.ToDynamicList(),
                totalCount,
                groupCount,
                summary,
            };
            return obj;
        }
        private static IQueryable QueryWhere(this IQueryable query, FiltroPaginate paginate)
        {
            if (paginate.filter == null) return query;
            string consulta = paginate.filter.ConsultaWhere();
            if (!string.IsNullOrEmpty(consulta)) query = query.Where(consulta);
            return query;
        }
        private static async Task<List<object>> QuerySummaryAsync(this IQueryable query, FiltroPaginate paginate, bool async)
        {
            if (paginate.totalSummary == null) return null;
            List<object> summary = new List<object>();
            foreach (var item in paginate.totalSummary)
            {
                object total = null;
                if (!string.IsNullOrEmpty(item.selector) && !string.IsNullOrEmpty(item.summaryType))
                {
                    try
                    {
                        if (item.summaryType == "sum") total = async ? await query.Select(item.selector).SumAsync() : query.Select(item.selector).Sum();
                        else if (item.summaryType == "avg") total = query.Aggregate("Average", item.selector);
                        else if (item.summaryType == "count") total = async ? await query.Select(item.selector).CountAsync() : query.Select(item.selector).Count();
                        else if (item.summaryType == "max") total = async ? await query.Select(item.selector).OrderBy($"{item.selector} desc").FirstOrDefaultAsync() : query.Select(item.selector).OrderBy($"{item.selector} desc").FirstOrDefault();
                        else if (item.summaryType == "min") total = async ? await query.Select(item.selector).OrderBy($"{item.selector} asc").FirstOrDefaultAsync() : query.Select(item.selector).OrderBy($"{item.selector} asc").FirstOrDefault();
                    }
                    catch { total = 0; }
                }
                summary.Add(total);
            }
            return summary;
        }
        private static IQueryable QueryOrderBy(this IQueryable query, FiltroPaginate paginate)
        {
            if (paginate.sort == null) return query;
            bool inicio = true;
            IOrderedQueryable queryOrden = (IOrderedQueryable)query.AsQueryable();
            foreach (var item in paginate.sort)
            {
                if (inicio) queryOrden = queryOrden.OrderBy($"{item.selector} {(item.desc ? "desc" : "asc")}");
                else queryOrden = queryOrden.ThenBy($"{item.selector} {(item.desc ? "desc" : "asc")}");
                inicio = false;
            }
            query = queryOrden.AsQueryable();
            return query;
        }
        private static IQueryable QueryGroupBy(this IQueryable query, FiltroPaginate paginate)
        {
            if (paginate.group == null) return query;
            string consulta = string.Empty, key = string.Empty, order = string.Empty;
            int contar = 0;
            foreach (var item in paginate.group)
            {
                if (item.groupInterval == "year") item.selector += ".Year";
                else if (item.groupInterval == "month") item.selector += ".Month";
                else if (item.groupInterval == "day") item.selector += ".Day";

                if (string.IsNullOrEmpty(consulta))
                {
                    consulta = "it.Key as key";
                    if (paginate.groupSummary != null)
                    {
                        List<string> summary = new List<string>();
                        foreach (var grup in paginate.groupSummary)
                        {
                            if (!string.IsNullOrEmpty(grup.selector) && !string.IsNullOrEmpty(grup.summaryType))
                            {
                                if (grup.summaryType == "count") summary.Add($"it.Select(it.{grup.selector}).Count().ToString()");
                                else if (grup.summaryType == "sum") summary.Add($"it.Select(it.{grup.selector}).Sum().ToString()");
                                else if (grup.summaryType == "avg") summary.Add($"it.Select(it.{grup.selector}).Average().ToString()");
                                else if (grup.summaryType == "max") summary.Add($"it.Select(it.{grup.selector}).OrderBy(it desc).FirstOrDefault().ToString()");
                                else if (grup.summaryType == "min") summary.Add($"it.Select(it.{grup.selector}).OrderBy(it asc).FirstOrDefault().ToString()");
                                else summary.Add("\"\"");
                            }
                            else summary.Add("\"\"");
                        }
                        if (summary.Any()) consulta += ",new[]{" + string.Join(",", summary.ToArray()) + "}.ToList() as summary";
                    }
                }
                else
                {
                    consulta += $", it.GroupBy({item.selector}).Select(new (it.Key as key";
                    contar++;
                }
                consulta += ",it.Count() as count";

                if (string.IsNullOrEmpty(key))
                {
                    key = item.selector;
                    order = item.desc != true ? "asc" : "desc";
                }
                if (!item.isExpanded) break;
            }
            consulta += ",\"\" as items";
            for (int i = 0; i < contar; i++) consulta += ")).OrderBy(key).ToList() as items";

            //if (contar > 0) query.Select($"new({string.Join(",", columna.Distinct().ToArray())})");
            query = query.GroupBy(key).Select($"new({consulta})").OrderBy($"key {order}");
            if (key.EndsWith("Year") && paginate.take == null) paginate.take = 5;
            return query;
        }
        private static IQueryable QuerySkipTake(this IQueryable query, FiltroPaginate paginate)
        {
            if (paginate.skip != null) query = query.Skip(paginate.skip.Value);
            if (paginate.take != null) query = query.Take(paginate.take.Value);
            return query;
        }
        private static async Task<dynamic> QueryCountAsync(this IQueryable query, FiltroPaginate paginate, bool async)
        {
            if (paginate.requireTotalCount != true) return null;
            return async ? await query.CountAsync() : query.Count();
        }
        private static async Task<dynamic> QueryGroupCountAsync(this IQueryable query, FiltroPaginate paginate, bool async)
        {
            if (paginate.requireGroupCount != true) return null;
            return async ? await query.CountAsync() : query.Count();
        }
        private static string ConsultaWhere(this object[] data)
        {
            string consulta = string.Empty;
            if (data == null) return consulta;
            string columna = string.Empty, operador = string.Empty, valor = string.Empty, union = string.Empty, recursiva = string.Empty;
            int c = 0;
            foreach (object item in data)
            {
                Type type = item != null ? item.GetType() : null;
                if (type == typeof(JArray) || type == typeof(Array))
                {
                    if (string.IsNullOrEmpty(union) && !string.IsNullOrEmpty(recursiva)) continue;
                    var recurso = JsonConvert.DeserializeObject<object[]>(JsonConvert.SerializeObject(item)).ConsultaWhere();
                    recursiva += (string.IsNullOrEmpty(recurso)) ? string.Empty : $" {union} ({recurso})";
                    c = 3;
                    union = string.Empty; columna = string.Empty; operador = string.Empty; valor = string.Empty;
                }
                else
                {
                    if (c == 0) columna = item.ToString();
                    else if (c == 1) operador = item.ToString();
                    else if (c == 2) valor = item != null ? item.ToString() : null;
                    else if (c == 3)
                    {
                        union = string.IsNullOrEmpty(recursiva) ? string.Empty : item.ToString();
                        c = -1;
                    }
                    c++;
                }

                if (c == 3) consulta += QueryString(columna, operador, valor);
            }
            consulta = $"{recursiva} {consulta}";
            return consulta.Trim();
        }
        private static string QueryString(string columna, string operador, string valor)
        {
            string consulta = string.Empty;
            if (string.IsNullOrEmpty(columna) || string.IsNullOrEmpty(operador)) return consulta;
            if (operador == "=" || string.IsNullOrEmpty(valor)) operador = " == " + (valor == null ? "null" : $"\"{valor}\"");
            else if (operador == ">" || operador == ">=" || operador == "<" || operador == "<=") operador = $" {operador} \"{valor}\"";
            else if (operador == "contains" && valor != null) operador = $".ToString().ToLower().Contains(\"{valor.ToLower()}\")";
            if (!string.IsNullOrEmpty(columna) && !string.IsNullOrEmpty(operador)) consulta += $"{columna}{operador} ";
            return consulta;
        }
        private class FiltroPaginate
        {
            public int? take { get; set; }
            public int? skip { get; set; }
            public bool? requireTotalCount { get; set; }
            public bool? requireGroupCount { get; set; }
            public List<SortFiltro> sort { get; set; }
            public List<GroupFiltro> group { get; set; }
            public object[] filter { get; set; }
            public List<Summary> totalSummary { get; set; }
            public List<Summary> groupSummary { get; set; }

        }
        private class SortFiltro
        {
            public string selector { get; set; }
            public bool desc { get; set; }
        }
        private class GroupFiltro
        {
            public string selector { get; set; }
            public string groupInterval { get; set; }
            public bool isExpanded { get; set; }
            public bool? desc { get; set; }
        }
        private class Summary
        {
            public string selector { get; set; }
            public string summaryType { get; set; }
        }
    }
}