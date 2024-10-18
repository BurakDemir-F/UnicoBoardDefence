using UnityEngine;

namespace Utilities
{
    public static class VectorExtensions
    {
        public static Vector3 SetX(this Vector3 @this,float xValue)
        {
            return new Vector3(xValue, @this.y, @this.z);
        }

        public static Vector3 SetY(this Vector3 @this,float yValue)
        {
            return new Vector3(@this.x, yValue, @this.z);
        }

        public static Vector3 SetZ(this Vector3 @this,float zValue)
        {
            return new Vector3(@this.x, @this.y, zValue);
        }
    }
}
