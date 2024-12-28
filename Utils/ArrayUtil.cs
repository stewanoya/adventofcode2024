using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class ArrayUtil
    {
        private static Dictionary<string, (int, int)> _directions = new()
        {
            { "N", (0, -1) },
            { "S", (0, 1) },
            { "W", (-1, 0) },
            { "E", (1, 0) },
            { "NW", (-1, -1) },
            { "NE", (1, -1) },
            { "SW", (-1, 1) },
            { "SE", (1, 1) },
        };

        public static (int, int) GetNextMove(string direction)
        {
            return _directions[direction];
        }

        public static bool MoveIsPossible<T>(int x, int y, int moveX, int moveY, IEnumerable<IEnumerable<T>> array)
        {

            var outerResult = array.ElementAtOrDefault(y + moveY);

            if (outerResult != null)
            {
                var innerResult = outerResult.ElementAtOrDefault(x + moveX);
                return innerResult != null && !innerResult.Equals(default(T));
            }

            return false;
        }

        public static void PrintMap<T>(IEnumerable<IEnumerable<T>> array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(string.Join(", ", item));
            }
        }
    }
}
