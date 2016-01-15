using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object o)
        {
            return (o == null);
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrEmpty<T>(this List<T> l)
        {
            return (l.IsNull() || !l.Any());
        }

        public static void ForEach<T>(this IEnumerable<T> query, Action<T> method)
        {
            foreach (T item in query)
            {
                method(item);
            }
        }
    }
}