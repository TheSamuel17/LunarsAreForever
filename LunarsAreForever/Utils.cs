using System.Linq;

namespace LunarsAreForever
{
    public static class Utils
    {
        public static T[] AddToArray<T>(this T[] array, params T[] items)
        {
            return (array ?? Enumerable.Empty<T>()).Concat(items).ToArray();
        }

        public static T[] RemoveFromArray<T>(this T[] array, params T[] items)
        {
            return (array ?? Enumerable.Empty<T>()).Except(items).ToArray();
        }
    }
}
