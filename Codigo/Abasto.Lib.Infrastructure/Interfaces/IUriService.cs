using Abasto.Lib.Core.QueryFilters;
using System;

namespace Abasto.Lib.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}