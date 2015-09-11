using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.Website.Models
{
    ///<exclude/>
    public interface IInfiniteList<T> : IEnumerable<T>
    {
        ///<exclude/>
        int Total { get; set; }
        ///<exclude/>
        int Skip { get; set; }
        ///<exclude/>
        IReadOnlyCollection<T> Items { get; }
        ///<exclude/>
        bool HasMore { get; }
    }

    ///<exclude/>
    public class InfiniteList<T> : IInfiniteList<T>
    {
        ///<exclude/>
        public int Total { get; set; }
        ///<exclude/>
        public int Skip { get; set; }
        ///<exclude/>
        public IReadOnlyCollection<T> Items { get; set; }
        ///<exclude/>
        public bool HasMore { get { return Skip < Total; } }

        ///<exclude/>
        public InfiniteList(int skip, int total, int pageSize, IEnumerable<T> source)
        {
            Total = total;
            Skip = skip + pageSize;
            Items = source != null ? source.ToList() : new List<T>();
        }

        ///<exclude/>
        public IEnumerator<T> GetEnumerator()
            => Items.GetEnumerator();

        ///<exclude/>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => Items.GetEnumerator();
    }

    ///<exclude/>
    public static class InfiniteListExtensions
    {
        ///<exclude/>
        public static IInfiniteList<T> ToInfiniteList<T>(this IEnumerable<T> source, int skip, int take)
            => new InfiniteList<T>(skip, source.Count(), take, source.Skip(skip).Take(take).ToList());

        ///<exclude/>
        public static async Task<IInfiniteList<T>> ToInfinateListAsync<T>(this IQueryable<T> source, int skip, int take)
            => await Task.Run(() => ToInfiniteList<T>(source, skip, take));
    }
}
