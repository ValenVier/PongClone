using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PerfromanceTest : MonoBehaviour
    {
        public int Iterations = 100000;

        private void Start()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
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
            Debug.Log($"Unity built-in: {watch.ElapsedMilliseconds}ms for {Iterations} iterations");
        }
    }
}