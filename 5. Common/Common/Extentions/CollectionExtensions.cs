﻿using System.Collections.Generic;

namespace Common.Extentions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }
    }
}
