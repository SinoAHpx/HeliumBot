using System.Collections.Generic;
using System.Linq;

namespace HeliumBot.Utils.Extensions
{
    public static class CollectionExtensions
    {
        public static bool Any<T>(this IEnumerable<T> ex, T match)
        {
            return ex.Any(e => e.Equals(match));
        }
    }
}