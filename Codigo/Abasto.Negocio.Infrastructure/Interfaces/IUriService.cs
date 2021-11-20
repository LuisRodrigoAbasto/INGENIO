using Abasto.Negocio.Core.QueryFilters;
using System;

namespace Abasto.Negocio.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}