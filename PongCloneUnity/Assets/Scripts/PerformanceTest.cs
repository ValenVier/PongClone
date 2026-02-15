using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace
{
    public class PerformanceTest : MonoBehaviour
    {
        [Header("Iterations")]
        public int Iterations = 100000;
        
        #region VECTOR3
        [Header("Vector3 - Basic")]
        public bool V3_Add;
        public bool V3_Sub;
        public bool V3_Scale;
        public bool V3_Divide;

        [Header("Vector3 - Magnitude")]
        public bool V3_Magnitude;
        public bool V3_MagnitudeSquared;
        public bool V3_Distance;
        public bool V3_DistanceSquared;

        [Header("Vector3 - Products")]
        public bool V3_Dot;
        public bool V3_Cross;

        [Header("Vector3 - Normalization")]
        public bool V3_Normalize;
        public bool V3_IsZero;

        [Header("Vector3 - Interpolation")]
        public bool V3_Lerp;
        public bool V3_Slerp;

        [Header("Vector3 - Angles & Projection")]
        public bool V3_Angle;
        public bool V3_ScalarProjection;
        public bool V3_VectorProjection;

        [Header("Vector3 - Movement")]
        public bool V3_Reflect;
        public bool V3_ClampMagnitude;
        public bool V3_DirectionTo;
        public bool V3_MoveTowards;
        public bool V3_Approximately;

        #endregion

        #region VECTOR2
        [Header("Vector2 - Basic")]
        public bool V2_Add;
        public bool V2_Sub;
        public bool V2_Scale;
        public bool V2_Divide;

        [Header("Vector2 - Magnitude")]
        public bool V2_Magnitude;
        public bool V2_MagnitudeSquared;
        public bool V2_Distance;

        [Header("Vector2 - Products")]
        public bool V2_Dot;
        public bool V2_Cross;

        [Header("Vector2 - Normalization")]
        public bool V2_Normalize;
        public bool V2_IsZero;

        [Header("Vector2 - Interpolation")]
        public bool V2_Lerp;
        public bool V2_Slerp;

        [Header("Vector2 - Angles & Projection")]
        public bool V2_Angle;
        public bool V2_ScalarProjection;
        public bool V2_VectorProjection;

        [Header("Vector2 - Movement")]
        public bool V2_Reflect;
        public bool V2_ClampMagnitude;
        public bool V2_DirectionTo;
        public bool V2_MoveTowards;
        public bool V2_Approximately;

        #endregion
        
        
        const float Epsilon = 1e-6f;
        
        //Helpers
        bool FloatEquals(float a, float b) => Mathf.Abs(a - b) < Epsilon;
        bool Vec2Equals(Vector2 a, Vector2 b) => Vector2.Distance(a, b) < Epsilon;
        bool Vec3Equals(Vector3 a, Vector3 b) => Vector3.Distance(a, b) < Epsilon;

        void Log(string operation, string type, object dllResult, object unityResult = null)
        {
            if (unityResult != null)
                Debug.Log($"{type}: {operation}: CustomDLL = {dllResult} | Unity = {unityResult}");
            else
                Debug.Log($"{type}: {operation}: CustomDLL = {dllResult}");
        }

        private void Start()
        {
            /*var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < Iterations; i++)
            {
                Vec3 a = new Vec3(1, 2, 3);
                Vec3 b = new Vec3(4,5,6);
                Vec3 result = VectorMath.Vector3Add(a, b);
            }
            
            watch.Stop();
            Debug.Log($"Custom DLL: {watch.ElapsedMilliseconds}ms for {Iterations} iterations");
            
            watch.Restart();

            for (int i = 0; i < Iterations; i++)
            {
                Vector3 a = new Vector3(1, 2, 3);
                Vector3 b = new Vector3(4,5,6);
                Vector3 result = a + b;
            }
            
            watch.Stop();
            Debug.Log($"Unity built-in: {watch.ElapsedMilliseconds}ms for {Iterations} iterations");*/
            
            Debug.Log("***** VECTOR MATH DLL TEST *****");
            TestVec3();
            TestVec2();

            Debug.Log("===== TESTS FINISHED =====");
        }
        
        void Measure(string name, Action dllAction, Action unityAction)
        {
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < Iterations; i++)
            {
                dllAction();
            }
            watch.Stop();
            var dllTime = watch.ElapsedMilliseconds;

            watch.Restart();
            for (int i = 0; i < Iterations; i++)
            {
                unityAction();
            }
            watch.Stop();
            var unityTime = watch.ElapsedMilliseconds;

            Debug.Log($"{name} | DLL: {dllTime}ms | Unity: {unityTime}ms | Iterations: {Iterations}");
        }
        
        //VEC3 TESTS
        void TestVec3()
        {
            Vec3 a = new Vec3(1, 2, 3);
            Vec3 b = new Vec3(4, 5, 6);

            Vector3 ua = a.ToUnityVector3();
            Vector3 ub = b.ToUnityVector3();

            if (V3_Add)
                Measure("V3 Add",
                    () => VectorMath.Vector3Add(a, b),
                    () => _ = ua + ub);

            if (V3_Sub)
                Measure("V3 Sub",
                    () => VectorMath.Vector3Subtraction(a, b),
                    () => _ = ua - ub);

            if (V3_Scale)
                Measure("V3 Scale",
                    () => VectorMath.Vector3Scale(a, 2f),
                    () => _ = ua * 2f);

            if (V3_Divide)
                Measure("V3 Divide",
                    () => VectorMath.Vector3Divide(a, 2f),
                    () => _ = ua / 2f);

            if (V3_Magnitude)
                Measure("V3 Magnitude",
                    () => VectorMath.Vector3Magnitude(a),
                    () => _ = ua.magnitude);

            if (V3_MagnitudeSquared)
                Measure("V3 MagnitudeSquared",
                    () => VectorMath.Vector3MagnitudeSquared(a),
                    () => _ = ua.sqrMagnitude);

            if (V3_Distance)
                Measure("V3 Distance",
                    () => VectorMath.Vector3Distance(a, b),
                    () => Vector3.Distance(ua, ub));

            if (V3_DistanceSquared)
                Measure("V3 DistanceSquared",
                    () => VectorMath.Vector3DistanceSquared(a, b),
                    () => _ = (ua - ub).sqrMagnitude);

            if (V3_Dot)
                Measure("V3 Dot",
                    () => VectorMath.Vector3Dot(a, b),
                    () => Vector3.Dot(ua, ub));

            if (V3_Cross)
                Measure("V3 Cross",
                    () => VectorMath.Vector3Cross(a, b),
                    () => Vector3.Cross(ua, ub));

            if (V3_Normalize)
                Measure("V3 Normalize",
                    () => VectorMath.Vector3Normalize(a),
                    () => _ = ua.normalized);

            if (V3_IsZero)
                Measure("V3 IsZero",
                    () => VectorMath.Vector3IsZero(a, Epsilon),
                    () => _ = ua.sqrMagnitude < Epsilon * Epsilon);

            if (V3_Lerp)
                Measure("V3 Lerp",
                    () => VectorMath.Vector3Lerp(a, b, 0.5f),
                    () => Vector3.Lerp(ua, ub, 0.5f));

            if (V3_Slerp)
                Measure("V3 Slerp",
                    () => VectorMath.Vector3Slerp(a, b, 0.5f),
                    () => Vector3.Slerp(ua.normalized, ub.normalized, 0.5f));

            if (V3_Angle)
                Measure("V3 Angle",
                    () => VectorMath.Vector3Angle(a, b),
                    () => Vector3.Angle(ua, ub));

            if (V3_ScalarProjection)
                Measure("V3 ScalarProjection",
                    () => VectorMath.Vector3ScalarProjection(a, b),
                    () => Vector3.Dot(ua, ub.normalized));

            if (V3_VectorProjection)
                Measure("V3 VectorProjection",
                    () => VectorMath.Vector3VectorProjection(a, b),
                    () => Vector3.Project(ua, ub));

            if (V3_Reflect)
                Measure("V3 Reflect",
                    () => VectorMath.Vector3Reflect(a, new Vec3(0,1,0)),
                    () => Vector3.Reflect(ua, Vector3.up));

            if (V3_ClampMagnitude)
                Measure("V3 ClampMagnitude",
                    () => VectorMath.Vector3ClampMagnitude(a, 2f),
                    () => Vector3.ClampMagnitude(ua, 2f));

            if (V3_DirectionTo)
                Measure("V3 DirectionTo",
                    () => VectorMath.Vector3DirectionTo(a, b, Epsilon),
                    () => _ = (ub - ua).normalized);

            if (V3_MoveTowards)
                Measure("V3 MoveTowards",
                    () => VectorMath.Vector3MoveTowards(a, b, 1f),
                    () => Vector3.MoveTowards(ua, ub, 1f));

            if (V3_Approximately)
                Measure("V3 Approximately",
                    () => VectorMath.Vector3Approximately(a, b, Epsilon),
                    () => _ = Vector3.Distance(ua, ub) < Epsilon);
        }

        //VEC2 TESTS
        void TestVec2()
        {
            Vec2 a = new Vec2(3, 4);
            Vec2 b = new Vec2(1, 2);

            Vector2 ua = a.ToUnityVector2();
            Vector2 ub = b.ToUnityVector2();

            if (V2_Add)
                Measure("V2 Add",
                    () => VectorMath.Vector2Add(a, b),
                    () => _ = ua + ub);

            if (V2_Sub)
                Measure("V2 Sub",
                    () => VectorMath.Vector2Subtraction(a, b),
                    () => _ = ua - ub);

            if (V2_Scale)
                Measure("V2 Scale",
                    () => VectorMath.Vector2Scale(a, 2f),
                    () => _ = ua * 2f);

            if (V2_Divide)
                Measure("V2 Divide",
                    () => VectorMath.Vector2Divide(a, 2f),
                    () => _ = ua / 2f);

            if (V2_Magnitude)
                Measure("V2 Magnitude",
                    () => VectorMath.Vector2Magnitude(a),
                    () => _ = ua.magnitude);

            if (V2_MagnitudeSquared)
                Measure("V2 MagnitudeSquared",
                    () => VectorMath.Vector2MagnitudeSquared(a),
                    () => _ = ua.sqrMagnitude);

            if (V2_Distance)
                Measure("V2 Distance",
                    () => VectorMath.Vector2Distance(a, b),
                    () => Vector2.Distance(ua, ub));

            if (V2_Dot)
                Measure("V2 Dot",
                    () => VectorMath.Vector2Dot(a, b),
                    () => Vector2.Dot(ua, ub));

            if (V2_Cross)
                Measure("V2 Cross",
                    () => VectorMath.Vector2Cross(a, b),
                    () => _ = ua.x * ub.y - ua.y * ub.x);

            if (V2_Normalize)
                Measure("V2 Normalize",
                    () => VectorMath.Vector2Normalize(a),
                    () => _ = ua.normalized);

            if (V2_IsZero)
                Measure("V2 IsZero",
                    () => VectorMath.Vector2IsZero(a, Epsilon),
                    () => _ = ua.sqrMagnitude < Epsilon * Epsilon);

            if (V2_Lerp)
                Measure("V2 Lerp",
                    () => VectorMath.Vector2Lerp(a, b, 0.5f),
                    () => Vector2.Lerp(ua, ub, 0.5f));

            if (V2_Slerp)
                Measure("V2 Slerp",
                    () => VectorMath.Vector2Slerp(a, b, 0.5f),
                    () => Vector2.Lerp(ua.normalized, ub.normalized, 0.5f));

            if (V2_Angle)
                Measure("V2 Angle",
                    () => VectorMath.Vector2Angle(a, b),
                    () => Vector2.Angle(ua, ub));

            if (V2_ScalarProjection)
                Measure("V2 ScalarProjection",
                    () => VectorMath.Vector2ScalarProjection(a, b),
                    () => Vector2.Dot(ua, ub.normalized));

            if (V2_VectorProjection)
                Measure("V2 VectorProjection",
                    () => VectorMath.Vector2VectorProjection(a, b),
                    () => _ = ub.sqrMagnitude > Mathf.Epsilon ? (Vector2.Dot(ua, ub) / ub.sqrMagnitude) * ub : Vector2.zero);

            if (V2_Reflect)
                Measure("V2 Reflect",
                    () => VectorMath.Vector2Reflect(a, new Vec2(0,1)),
                    () => Vector2.Reflect(ua, Vector2.up));

            if (V2_ClampMagnitude)
                Measure("V2 ClampMagnitude",
                    () => VectorMath.Vector2ClampMagnitude(a, 2f),
                    () => Vector2.ClampMagnitude(ua, 2f));

            if (V2_DirectionTo)
                Measure("V2 DirectionTo",
                    () => VectorMath.Vector2DirectionTo(a, b, Epsilon),
                    () => _ = (ub - ua).normalized);

            if (V2_MoveTowards)
                Measure("V2 MoveTowards",
                    () => VectorMath.Vector2MoveTowards(a, b, 1f),
                    () => Vector2.MoveTowards(ua, ub, 1f));

            if (V2_Approximately)
                Measure("V2 Approximately",
                    () => VectorMath.Vector2Approximately(a, b, Epsilon),
                    () => _ = Vector2.Distance(ua, ub) < Epsilon);
        }
    }
}