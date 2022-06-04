using UnityEngine;

namespace ValheimWards.Extensions
{
    public static class Vector3Extensions
    {
        public static bool WithinDistance(this Vector3 origin, Vector3 pointToTest, double distance) => Vector3.Distance(origin, pointToTest) <= distance;
    }
}
