using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using EntityFramework.DynamicLinq;

namespace Abasto.Negocio.Api.Controllers
{
    public static partial class QueryableExtensions
    {
        public static async Task<dynamic> PaginateAsync<T>(this IQueryable<T> source, string filtro) where T : class
        {
            int? totalCount = null;
            List<dynamic> summary = new List<dynamic>();
            IQueryable query = source.AsQueryable();
            if (!string.IsNullOrEmpty(filtro))
            {
                var paginate = Newtonsoft.Json.JsonConvert.DeserializeObject<FiltroPaginate>(filtro);
                query = QueryWhere(query, paginate);
                summary = await QuerySummaryAsync(query, paginate);
                query = QueryOrderBy(query, paginate);
                query = QueryGroupBy(query, paginate);
                if (paginate.requireTotalCount == true) totalCount = await ((IQueryable<T>)query.AsQueryable()).CountAsync();
                if (paginate.take != null && paginate.skip != null) query = query.Skip(paginate.skip.Value).Take(paginate.take.Value);
            }
            return new
            {
                data = await query.ToDynamicListAsync(),
                totalCount,
                summary,
            };
        }

        public static dynamic Paginate<T>(this IQueryable<T> source, string filtro) where T : class
        {
            int? totalCount = null;
            List<dynamic> summary = null;
            IQueryable query = source.AsQueryable();
            if (!string.IsNullOrEmpty(filtro))
            {
                var paginate = Newtonsoft.Json.JsonConvert.DeserializeObject<FiltroPaginate>(filtro);
                query = QueryWhere(query, paginate);
                summary = QuerySummary(query, paginate);
                query = QueryOrderBy(query, paginate);
                query = QueryGroupBy(query, paginate);
                if (paginate.requireTotalCount == true) totalCount = ((IQueryable<T>)query.AsQueryable()).Count();
                if (paginate.take != null && paginate.skip != null) query = query.Skip(paginate.skip.Value).Take(paginate.take.Value);
            }
            return new
            {
                data = query.ToDynamicList(),
                totalCount,
                summary,
            };
        }
        private static IQueryable QueryWhere(this IQueryable query, FiltroPaginate paginate)
        {
            if (paginate.filter != null)
            {
                string consulta = string.Empty, siguiente = string.Empty;
                foreach (var item in paginate.filter)
                {
                    string operador = string.Empty, valor = item.valor == null ? null : item.valor.ToLower();
                    if (item.operador == "=" || string.IsNullOrEmpty(valor)) operador = $" == \"{valor}\"";
                    else if (item.operador == ">" || item.operador == ">=" || item.operador == "<" || item.operador == "<=") operador = $" {item.operador} \"{valor}\"";
                    else if (item.operador == "contains") operador = $".ToString().ToLower().Contains(\"{valor}\")";
                    else if (item.operador == "or") operador = $" {item.operador} \"{valor}\"";
                    if (!string.IsNullOrEmpty(operador))
                    {
                        consulta += $"{siguiente} {item.columna}{operador} ";
                        siguiente = item.siguiente;
                    }
                }
                if (!string.IsNullOrEmpty(consulta)) query = query.Where(consulta);
            }
            return query;
        }
        private static List<dynamic> QuerySummary(this IQueryable query, FiltroPaginate paginate)
        {
            List<dynamic> summary = new List<dynamic>();
            if (paginate.totalSummary != null)
            {
                foreach (var item in paginate.totalSummary)
                {
                    dynamic total = 0;
                    if (!string.IsNullOrEmpty(item.selector) && !string.IsNullOrEmpty(item.summaryType))
                    {
                        try
                        {
                            if (item.summaryType == "sum") total = query.Sum(item.selector);
                            else if (item.summaryType == "count") total = query.Count(item.selector);
                            else if (item.summaryType == "avg") total = query.Average(item.selector);
                        }
                        catch { total = 0; }
                    }
                    summary.Add(total);
                }
            }
            return summary;
        }
        private static async Task<List<dynamic>> QuerySummaryAsync(this IQueryable query, FiltroPaginate paginate)
        {
            List<dynamic> summary = new List<dynamic>();
            if (paginate.totalSummary != null)
            {
                foreach (var item in paginate.totalSummary)
                {
                    dynamic total = 0;
                    if (!string.IsNullOrEmpty(item.selector) && !string.IsNullOrEmpty(item.summaryType))
                    {
                        try
                        {
                            if (item.summaryType == "sum") total = await ((IQueryable<dynamic>)query).SumAsync(item.selector);
                            else if (item.summaryType == "count") total = await ((IQueryable<dynamic>)query).CountAsync(item.selector);
                            else if (item.summaryType == "avg") total = await ((IQueryable<dynamic>)query).AverageAsync(item.selector);
                        }
                        catch { total = 0; }
                    }
                    summary.Add(total);
                }
            }
            return summary;
        }
        private static IQueryable QueryOrderBy(this IQueryable query, FiltroPaginate paginate)
        {
            if (paginate.sort != null)
            {
                bool inicio = true;
                IOrderedQueryable queryOrden = (IOrderedQueryable)query.AsQueryable();
                foreach (var item in paginate.sort)
                {
                    if (inicio) queryOrden = queryOrden.OrderBy($"{item.selector} {(item.desc ? "desc" : "asc")}");
                    else queryOrden = queryOrden.ThenBy($"{item.selector} {(item.desc ? "desc" : "asc")}");
                    inicio = false;
                }
                query = queryOrden.AsQueryable();
            }
            return query;
        }
        private static IQueryable QueryGroupBy(this IQueryable query, FiltroPaginate paginate)
        {
            if (paginate.group != null)
            {
                string consulta = string.Empty, key = string.Empty;
                int contar = 0; List<string> columna = new List<string>();
                foreach (var item in paginate.group)
                {
                    columna.Add(item.selector);
                    if (item.groupInterval == "year") item.selector += ".Year";
                    else if (item.groupInterval == "month") item.selector += ".Month";
                    else if (item.groupInterval == "day") item.selector += ".Day";

                    if (string.IsNullOrEmpty(consulta)) consulta = "it.Key as key";
                    else consulta += $", it.GroupBy({item.selector}).Select(new (it.Key as key";

                    if (string.IsNullOrEmpty(key)) key = item.selector;
                    if (item.isExpanded) contar++;
                    if (item.groupInterval == "year")
                    {
                        contar--;
                        break;
                    }
                }
                for (int i = 0; i < contar; i++) consulta += ")).OrderBy(key).ToList() as items";
                if (contar > 0) query.Select($"new({string.Join(",", columna.Distinct().ToArray())})");
                query = query.GroupBy(key).Select($"new({consulta})").OrderBy($"key {(key.EndsWith("Year") ? "desc" : "asc")}");
            }
            return query;
        }
        private class FiltroPaginate
        {
            public int? take { get; set; }
            public int? skip { get; set; }
            public bool? requireTotalCount { get; set; }
            public List<SortFiltro> sort { get; set; }
            public List<GroupFiltro> group { get; set; }
            public List<Filtro> filter { get; set; }
            public List<TotalSummary> totalSummary { get; set; }

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
        }
        private class Filtro
        {
            public string columna { get; set; }
            public string operador { get; set; }
            public string valor { get; set; }
            public string siguiente { get; set; }
        }
        private class TotalSummary
        {
            public string selector { get; set; }
            public string summaryType { get; set; }
        }
    }
}