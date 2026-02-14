using System.Runtime.InteropServices;

namespace DefaultNamespace
{
    public static class VectorMath
    {
        private const string DLLName = "VectorMathematics";

        // Basic Operations
        [DllImport(DLLName)]
        public static extern Vec3 Vector3Add(Vec3 a, Vec3 b);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Subtraction(Vec3 a, Vec3 b);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Scale(Vec3 a, float scale);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Divide(Vec3 a, float scalar);

        // Magnitude and Distance
        [DllImport(DLLName)]
        public static extern float Vector3Magnitude(Vec3 v);

        [DllImport(DLLName)]
        public static extern float Vector3MagnitudSquared(Vec3 v);

        [DllImport(DLLName)]
        public static extern float Vector3Distance(Vec3 a, Vec3 b);

        [DllImport(DLLName)]
        public static extern float Vector3DistanceSquared(Vec3 a, Vec3 b);

        // Dot and Cross
        [DllImport(DLLName)]
        public static extern float Vector3Dot(Vec3 a, Vec3 b);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Cross(Vec3 a, Vec3 b);

        // Normalization
        [DllImport(DLLName)]
        public static extern Vec3 Vector3Normalize(Vec3 v);

        [DllImport(DLLName)]
        [return: MarshalAs(UnmanagedType.I1)] // bool native C++
        public static extern bool Vector3IsZero(Vec3 v, float epsilon);

        // Interpolation
        [DllImport(DLLName)]
        public static extern Vec3 Vector3Lerp(Vec3 a, Vec3 b, float t);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Slerp(Vec3 a, Vec3 b, float t);

        // Angles and Projections
        [DllImport(DLLName)]
        public static extern float Vector3Angle(Vec3 a, Vec3 b);

        [DllImport(DLLName)]
        public static extern float Vector3ScalarProjection(Vec3 a, Vec3 b);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3VectorProjection(Vec3 a, Vec3 b);

        // Reflection and Movement
        [DllImport(DLLName)]
        public static extern Vec3 Vector3Reflect(Vec3 v, Vec3 normal);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3ClampMagnitude(Vec3 v, float maxLen);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3DirectionTo(Vec3 from, Vec3 to, float epsilon);

        [DllImport(DLLName)]
        public static extern Vec3 Vector3MoveTowards(Vec3 current, Vec3 target, float maxDistance);


        // Utility
        [DllImport(DLLName)]
        [return: MarshalAs(UnmanagedType.I1)] // bool native C++
        public static extern bool Vector3Approximately(Vec3 a, Vec3 b, float epsilon);
        
        // Constants
        [DllImport(DLLName)]
        public static extern Vec3 Vector3Zero();

        [DllImport(DLLName)]
        public static extern Vec3 Vector3One();

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Right();

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Up();

        [DllImport(DLLName)]
        public static extern Vec3 Vector3Forward();
    }
}