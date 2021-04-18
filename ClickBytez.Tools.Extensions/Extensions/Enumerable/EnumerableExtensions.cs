using System;
using System.Collections.Generic;

namespace ClickBytez.Tools.Enumerable
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TObjectType>(this IEnumerable<TObjectType> @this, Action<TObjectType> action) 
        {
            foreach (TObjectType @object in @this) action(@object);
        }

        public static void ForEach<TObjectType>(this IEnumerable<TObjectType> @this, Action<TObjectType, int> action)
        {
            int index = 0;
            foreach (TObjectType @object in @this) action(@object, index++);
        }
    }
}
