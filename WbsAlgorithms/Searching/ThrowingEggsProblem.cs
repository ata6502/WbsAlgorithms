namespace WbsAlgorithms.Searching
{
    public class ThrowingEggsProblem
    {
        public static int GetHighestFloor(bool[] floors)
        {
            return GetHighestFloorRecursive(floors, 0, floors.Length - 1);
        }

        private static int GetHighestFloorRecursive(bool[] floors, int low, int high)
        {
            if (low > high)
            {
                if (high == -1)
                    return high;
                else if (low == floors.Length)
                    return low;
                else
                    return high + 1;
            }

            var mid = (low + high) / 2;

            if (floors[mid])
                return GetHighestFloorRecursive(floors, mid + 1, high);
            else
                return GetHighestFloorRecursive(floors, low, mid - 1);
        }
    }
}
