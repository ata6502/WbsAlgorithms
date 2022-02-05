using System;
using System.Collections;

namespace WbsAlgorithms.Searching
{
    /// <summary>
    /// [Sedgewick] 1.4.24 p.211 - Throwing eggs from a building.
    /// You have an N-story building. An egg is broken if it is thrown off floor F or higher,
    /// and intact otherwise. Devise a strategy to determine the value of F such that the number
    /// of broken eggs ~lgN when using ~lgN throws, then find a way to reduce the cost to ~2lgF.
    /// 
    /// For the purpose of concise description, we define two types of floors:
    /// - "egg intact floor": an egg remains intact if it is thrown from this floor
    /// - "egg broken floor": an egg breaks if it is thrown from this floor
    /// 
    /// Reference: http://stackoverflow.com/questions/17404642/throwing-eggs-from-a-building
    /// </summary>
    public class EggThrowingProblem
    {
        /// <summary>
        /// Determines the highest "egg intact floor". The method uses an ordinary binary search.
        /// As such, it "throws eggs" ~lgN times.
        /// </summary>
        /// <param name="floors">
        /// A set of Boolean values representing the floors in a building. The floors are numbered
        /// from 1 to N. An egg thrown from a floor marked as true remains intact. An egg thrown 
        /// from a floor marked as false breaks
        /// </param>
        /// <returns>
        /// The highest "egg intact floor". If a floor like that has not been found, the method returns -1.
        /// </returns>
        public static int GetHighestFloor(BitArray floors)
        {
            return GetHighestFloorRecursive(floors, 0, floors.Length - 1);
        }

        private static int GetHighestFloorRecursive(BitArray floors, int low, int high)
        {
            if (low > high)
            {
                // The highest floor reached but an egg would break when thrown from it.
                if (high == -1)
                    return high;
                // The lowest floor reached.
                else if (low == floors.Length)
                    return low;
                // We have found the highest floor from which an egg is thrown and it remains intact.
                else
                    return high + 1;
            }

            // Jump in the middle of the [low,high] range just like in the binary search.
            var mid = (low + high) / 2;

            // Continue search in the upper or lower part of the building.
            if (floors[mid])
                return GetHighestFloorRecursive(floors, mid + 1, high);
            else
                return GetHighestFloorRecursive(floors, low, mid - 1);
        }

        /// <summary>
        /// Determines the highest "egg intact floor". As the first step, the method narrows down 
        /// the number of floors and then performs a binary search on this subset of floors.
        /// Thanks to that, it "throws eggs" only ~2lgF times.
        /// </summary>
        /// <param name="floors">
        /// A set of Boolean values representing the floors in a building. The floors are numbered
        /// from 1 to N. An egg thrown from a floor marked as true remains intact. An egg thrown 
        /// from a floor marked as false breaks
        /// </param>
        /// <returns>
        /// The highest "egg intact floor". If a floor like that has not been found, the method returns -1.
        /// </returns>
        public static int GetHighestFloorFaster(BitArray floors)
        {
            var low = 1;
            var high = 1;

            // Find a subset of floors by starting from the first floor and moving up until 
            // an "egg broken floor" is found. We find this floor by doubling the floor number
            // with each iteration.
            while (floors[high - 1] && high < floors.Length)
            {
                low = high;
                high = Math.Max(high << 1, floors.Length);
            }

            // Perform a binary search on the subset of floors.
            return GetHighestFloorRecursive(floors, low - 1, high - 1);
        }
    }
}
