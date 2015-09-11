using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.Website.Models
{
    ///<exclude />
    public interface IPagedListParameters
    {
        ///<exclude />
        int Page { get; }
        ///<exclude />
        int PageSize { get; }
    }

    ///<exclude />
    public class PagedListParameters : IPagedListParameters
    {
        ///<exclude />
        public int Page { get; }
        ///<exclude />
        public int PageSize { get; }

        ///<exclude />
        public PagedListParameters(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }

    ///<exclude />
    public interface IPagedList<T> : IEnumerable<T>, IPagedListParameters
    {
        ///<exclude />
        IReadOnlyCollection<T> Items { get; }
        ///<exclude />
        int TotalCount { get; }
    }

    ///<exclude />
    public class PagedList<T> : IPagedList<T>
    {
        ///<exclude />
        public int Page { get; }
        ///<exclude />
        public int PageSize { get; }
        ///<exclude />
        public int TotalCount { get; }
        ///<exclude />
        public IReadOnlyCollection<T> Items { get; }
        ///<exclude />
        public PagedList(int page, int pageSize, int totalCount, IEnumerable<T> source)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = source != null ? source.ToList() : new List<T>();
        }
        ///<exclude />
        public IEnumerator<T> GetEnumerator() { return Items.GetEnumerator(); }
        ///<exclude />
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return Items.GetEnumerator(); }
    }

    ///<exclude />
    public static class PagedListExtensions
    {
        ///<exclude />
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int page, int pageSize)
            => ToPagedList<T>(source.AsQueryable(), page, pageSize);

        ///<exclude />
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int page, int pageSize)
        {
            var count = source.Count();
            if ((page < 1) || ((page - 1) * pageSize >= count))
                page = 1;

            return new PagedList<T>(
                page,
                pageSize,
                count,
                source.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        ///<exclude />
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IEnumerable<T> source, int page, int pageSize)
            => await ToPagedListAsync<T>(source.AsQueryable(), page, pageSize);

        ///<exclude />
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int page, int pageSize)
            => await Task.Run(() => ToPagedList<T>(source, page, pageSize));
    }
}
