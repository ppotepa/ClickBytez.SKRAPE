using System;
using System.Collections.Generic;

namespace ClickBytez.Tools.Enumerable
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TObjectType>(this IEnumerable<TObjectType> @this, Action<TObjectType> action) 
        {
            foreach (TObjectType index in @this)
            {
                action(index);
            }
        }
    }
}
