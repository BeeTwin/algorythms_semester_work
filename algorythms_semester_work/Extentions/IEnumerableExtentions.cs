using System;
using System.Collections.Generic;

namespace algorythms_semester_work
{
    public static class IEnumerableExtentions
    {
        /// <summary>
        /// Executes an <paramref name="action"/> for each element in <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
            return collection;
        }
    }
}