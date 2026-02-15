using DefaultNamespace;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Vec3 position;
    public bool useDynamicBounce = false;
    
    public void ResetPosition()
    {
        position.y = -0.75f;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Paddle collision");
        if (!collision.gameObject.TryGetComponent(out BallController ball))
            return;

        Vec3 ballVelocity = ball.GetVelocity();

        // Positions
        Vec3 paddlePos = new Vec3(transform.position.x, transform.position.y, transform.position.z);
        Vec3 ballPos = ball.GetPosition();
        
        //Normal vector
        Vec3 normal = VectorMath.Vector3Subtraction(ballPos, paddlePos);
        normal = VectorMath.Vector3Normalize(normal);

        // Mathematical reflection
        Vec3 reflected = VectorMath.Vector3Reflect(ballVelocity, normal);

        if (useDynamicBounce)
        {
            float paddleHeight = GetComponent<BoxCollider>().bounds.size.y;
            float offset = ball.GetPosition().y - position.y;

            float influence = offset / paddleHeight;

            Vec3 upInfluence = new Vec3(0f, influence * 5f, 0f);
            reflected = VectorMath.Vector3Add(reflected, upInfluence);
        }

        ball.SetVelocity(reflected);
    }
}
