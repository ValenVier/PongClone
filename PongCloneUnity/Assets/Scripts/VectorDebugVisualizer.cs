using UnityEngine;

namespace DefaultNamespace
{
    public class VectorDebugVisualizer : MonoBehaviour
    {
        public Transform pointA;
        public Transform pointB;

        public bool showAddition;
        public bool showSubtraction;
        
        void OnDrawGizmos()
        {
            if (pointA == null || pointB == null) return;
            
            Vec3 a = Vec3.FromUnityVector3(pointA.position);
            Vec3 b = Vec3.FromUnityVector3(pointB.position);
            
            Gizmos.color = Color.darkRed;
            Gizmos.DrawWireSphere(pointA.position, 2f);
            
            Gizmos.color = Color.deepSkyBlue;
            Gizmos.DrawWireSphere(pointB.position, 2f);

            if (showAddition)
            {
                Vec3 sum = VectorMath.Vector3Add(a, b);
                Gizmos.color = Color.greenYellow;
                Gizmos.DrawWireSphere(sum.ToUnityVector3(), 2f);
                Gizmos.DrawLine(Vector3.zero, sum.ToUnityVector3());
            }
            
            if (showSubtraction)
            {
                Vec3 diff = VectorMath.Vector3Subtraction(a, b);
                Gizmos.color = Color.darkOrange;
                Gizmos.DrawRay(pointA.position, diff.ToUnityVector3());
            }
        }
        
    }
}