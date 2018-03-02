using System.Linq;

namespace TwitterDemo.Code
{
    public static class Extensions
    {
        public static bool IsOneOf<T>(this T item, params T[] items)
        {
            return items.Contains(item);
        }
    }
}
