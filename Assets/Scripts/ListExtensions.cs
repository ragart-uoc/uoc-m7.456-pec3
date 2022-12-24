using System;
using System.Collections.Generic;

namespace PEC3
{
    /// <summary>
    /// Class <c>ListExtensions</c> contains extensions for the List class.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Method <c>Shuffle</c> shuffles the list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            var rnd = new Random();
            for (var i = 0; i < list.Count; i++)
                list.Swap(i, rnd.Next(i, list.Count));
        }

        /// <summary>
        /// Method <c>Swap</c> swaps two elements in the list.
        /// </summary>
        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}