using System.Collections.Generic;

namespace Abasto.Library.DevExtreme.Config
{
    public class FilterClient
    {
        public object[]? key { get; set; }
        public string? dataField { get; set; }
        public int? take { get; set; }
        public int? skip { get; set; }
        public bool? isLoadingAll { get; set; }
        public bool? requireTotalCount { get; set; }
        public bool? requireGroupCount { get; set; }
        public List<SortFilter>? sort { get; set; }
        public List<GroupFilter>? group { get; set; }
        public object[]? filter { get; set; }
        public List<Summary>? totalSummary { get; set; }
        public List<Summary>? groupSummary { get; set; }
        //esto es para SelectBox,Autocomplete,DrowBox
        public object? searchExpr { get; set; }
        public object? searchOperation { get; set; }
        public object? searchValue { get; set; }

    }
    public class SortFilter
    {
        public string? selector { get; set; }
        public bool desc { get; set; }
    }
    public class GroupFilter
    {
        public string? selector { get; set; }
        public object? groupInterval { get; set; }
        public bool isExpanded { get; set; }
        public bool? desc { get; set; }
    }
    public class Summary
    {
        public string? selector { get; set; }
        public string? summaryType { get; set; }
    }
    public class SummaryItem
    {
        public int orden { get; set; }
        public string? selector { get; set; }
        public string? summaryType { get; set; }
        public object? value { get; set; }

    }
}
