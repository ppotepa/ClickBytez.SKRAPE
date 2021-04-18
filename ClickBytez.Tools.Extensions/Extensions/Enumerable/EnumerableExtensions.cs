using System;
using System.Collections.Generic;

namespace ClickBytez.Tools.Extensions.Enumerable
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

        public static TimeSpan DiagnostictsForEach<TObjectType>(this IEnumerable<TObjectType> @this, Action<TObjectType, int> action)
        {
            DateTime start = DateTime.Now;
            int index = 0;
            foreach (TObjectType @object in @this) 
            {
                action(@object, index++);
            }
            DateTime end = DateTime.Now;
            return (start - end);
        }
    }
}
