using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace eGym.Common
{
    public static class Linq
    {
        #region OBSERVABLE COLLECTION
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> source)
        {
            if (source == null || source.Count() <= 0) return null;
            return new ObservableCollection<T>(source);
        }

        public static ObservableCollection<T> ToObservable<T>(this IOrderedEnumerable<T> source)
        {
            if (source == null || source.Count() <= 0) return null;
            return new ObservableCollection<T>(source);
        }

        public static ObservableCollection<T> ToObservable<T>(this IList<T> source)
        {
            if (source == null || source.Count() <= 0) return null;
            return new ObservableCollection<T>(source);
        }

        public static ObservableCollection<T> ToObservable<T>(this IQueryable<T> source)
        {
            if (source == null || source.Count() <= 0) return null;
            return new ObservableCollection<T>(source);
        }

        public static ObservableCollection<T> ToObservable<T>(this IOrderedQueryable<T> source)
        {
            if (source == null || source.Count() <= 0) return null;
            return new ObservableCollection<T>(source);
        }
        #endregion

        #region ORDER BY
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey1, TKey2>(this IEnumerable<TSource> source, Func<TSource, TKey1> key1, Func<TSource, TKey2> key2)
        {
            if (source == null || !source.Any()) return null;
            return source.OrderBy(key1).ThenBy(key2);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey1, TKey2>(this IQueryable<TSource> source, Func<TSource, TKey1> key1, Func<TSource, TKey2> key2)
            => (source.AsEnumerable()).OrderBy(key1: key1, key2: key2);

        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey1, TKey2>(this IEnumerable<TSource> source, Func<TSource, TKey1> key1, Func<TSource, TKey2> key2)
        {
            if (source == null || !source.Any()) return null;
            return source.OrderByDescending(key1).ThenByDescending(key2);
        }

        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey1, TKey2>(this IQueryable<TSource> source, Func<TSource, TKey1> key1, Func<TSource, TKey2> key2)
            => (source.AsEnumerable()).OrderByDescending(key1: key1, key2: key2);
        #endregion

        #region DISTINCT BY
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> func)
        {
            if (source == null || !source.Any()) return null;
            return source.GroupBy(func).Select(o => o.FirstOrDefault());
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> func)
            => (source.AsEnumerable()).DistinctBy(func: func);

        public static IEnumerable<TSource> DistinctByOrdered<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> distinctFunc, Func<TSource, TKey> orderFunc, bool descending = false)
        {
            if (source == null || !source.Any()) return null;
            IQueryable<TSource> result = null;
            if (descending) result = source.OrderByDescending(orderFunc).AsQueryable();
            else result = source.OrderBy(orderFunc).AsQueryable();
            return result.OrderBy(orderFunc).GroupBy(distinctFunc).Select(o => o.FirstOrDefault());
        }

        public static IEnumerable<TSource> DistinctByOrdered<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> distinctFunc, Func<TSource, TKey> orderFunc, bool descending = false)
            => (source.AsEnumerable()).DistinctByOrdered(distinctFunc: distinctFunc, orderFunc: orderFunc, descending: descending);
        #endregion
    }
}
