using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class VectorMathTest : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("**** Testing Vector Math DLL ****");

            Vec3 a = new Vec3(1, 2, 3);
            Vec3 b = new Vec3(4, 5, 6);
            
            Vec3 result = VectorMath.Vector3Add(a, b);
            Debug.Log($"Vector add: ({a}) + ({b}) = ({result})");
            
            Vector3 resultUnityVector = result.ToUnityVector3();
            Debug.Log($"Vector resultUnityVector: = {resultUnityVector}");
            
            Debug.Log("Test move object to result position");
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Debug.Log($"Test object starting position: {go.transform.position}");
            go.transform.position = result.ToUnityVector3();
            Debug.Log($"Test object new position: {go.transform.position}");
        }
    }
}