using DefaultNamespace;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    public enum ForceType
    {
        Additive,
        Multiplicative,
    }

    public ForceType forceType = ForceType.Additive;
    public float bounceStrength = 0f;
    
    public enum WallType { HorizontalTop, HorizontalBottom, VerticalLeft, VerticalRight }
    public WallType wallType;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Wall collision");

        if (collision.gameObject.TryGetComponent(out BallController ball))
        {
            // adjust speed
            switch (forceType)
            {
                case ForceType.Additive:
                    ball.currentSpeed += bounceStrength;
                    break;

                case ForceType.Multiplicative:
                    ball.currentSpeed *= bounceStrength;
                    break;
            }

            // Normal
            Vec3 normal;
            switch (wallType)
            {
                case WallType.HorizontalTop: normal = new Vec3(0f, -1f, 0f); break;
                case WallType.HorizontalBottom: normal = new Vec3(0f, 1f, 0f); break;
                case WallType.VerticalLeft: normal = new Vec3(1f, 0f, 0f); break;
                case WallType.VerticalRight: normal = new Vec3(-1f, 0f, 0f); break;
                default: normal = new Vec3(0f, 1f, 0f); break;
            }

            // Reflect
            Vec3 reflected = VectorMath.Vector3Reflect(ball.GetVelocity(), normal);

            // Limit speed to the maximum
            ball.SetVelocity(VectorMath.Vector3ClampMagnitude(reflected, ball.currentSpeed));
        }
    }
}
