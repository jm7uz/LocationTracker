using LocationTracker.Domain.Commons;
using LocationTracker.Service.Commons.Helpers;
using LocationTracker.Service.Configurations;
using LocationTracker.Service.Exceptions;
using Newtonsoft.Json;

namespace LocationTracker.Service.Commons.Extentions;

public static class CollectionExtentions
{
    public static IQueryable<TEntity> ToPagedList<TEntity, TKey>(this IQueryable<TEntity> source, PaginationParams @params)
            where TEntity : Auditable<TKey>
    {

        var metaData = new PaginationMetaData(source.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);
        if (HttpContextHelper.ResponseHeaders != null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        }

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
        source
            .OrderBy(s => s.Id)
            .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : throw new LocationTrackerException(400, "Please, enter valid numbers");
    }
}
