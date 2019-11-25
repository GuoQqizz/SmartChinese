using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Code.Extension
{
    public static class ListExtensions
    {
        public static List<T> RandomSortList<T>(this List<T> source)
        {
            Random random = new Random();
            List<T> newList = new List<T>();
            foreach (T item in source)
            {
                newList.Insert(random.Next(newList.Count + 1), item);
            }
            return newList;
        }

        public static List<T> ExceptExt<T, TKey>(this List<T> first, List<T> second, Func<T, TKey> keySelector) where T : class, new() where TKey : IComparable<TKey>
        {
            var res = first.Except(second, new TEqualityComparer<T, TKey>(keySelector)).ToList();
            return res;
        }
    }

    public class TEqualityComparer<T, TKey> : IEqualityComparer<T> where T : class, new() where TKey : IComparable<TKey>
    {
        private Func<T, TKey> keySelector;
        public TEqualityComparer(Func<T, TKey> keySelector)
        {
            this.keySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            //count += 1;
            //var type = typeof(T);
            //var vx = keySelector(x);
            //var vy = keySelector(y);
            //var res = vx.Equals(vy);
            return keySelector(x).Equals(keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}
