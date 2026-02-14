using System.Runtime.InteropServices;
using UnityEngine;

namespace DefaultNamespace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec3
    {
        public float x;
        public float y;
        public float z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vec3 FromUnityVector3(Vector3 v)
        {
            return new Vec3(v.x, v.y, v.z);
        }

        public Vector3 ToUnityVector3()
        {
            return new Vector3(x, y, z); 
        }

        public override string ToString()
        {
            return $"{x}, {y}, {z}";
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec2
    {
        public float x;
        public float y;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2 FromUnityVector3(Vector2 v)
        {
            return new Vec2(v.x, v.y);
        }

        public Vector2 ToUnityVector2()
        {
            return new Vector2(x, y); 
        }

        public Vector3 ToUnityVector3()
        {
            return new Vector3(x, y, 0);
        }
        
        public override string ToString()
        {
            return $"{x}, {y}";
        }
    }
}