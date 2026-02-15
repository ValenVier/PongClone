using DefaultNamespace;
using UnityEngine;


public class BallController : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float maxSpeed = float.PositiveInfinity;

    public float currentSpeed { get; set; }

    private Vec3 position;
    private Vec3 velocity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetPosition();
        ResetPosition();
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        // position += velocity * dt
        Vec3 frameVelocity = VectorMath.Vector3Scale(velocity, dt);
        position = VectorMath.Vector3Add(position, frameVelocity);

        transform.position = new Vector3(position.x, position.y, position.z);
    }

    public void ResetPosition()
    {
        position = VectorMath.Vector3Zero();
        velocity = VectorMath.Vector3Zero();
        currentSpeed = 0f;

        position.y = -0.75f;
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1f : 1f;

        float y = Random.value < 0.5f
            ? Random.Range(-1f, -0.5f)
            : Random.Range(0.5f, 1f);

        Vec3 direction = new Vec3(x, y, 0f);
        direction = VectorMath.Vector3Normalize(direction);

        velocity = VectorMath.Vector3Scale(direction, baseSpeed);
        currentSpeed = baseSpeed;
    }

    public Vec3 GetVelocity() => velocity;
    public Vec3 GetPosition() => position;

    public void SetVelocity(Vec3 v)
    {
        velocity = VectorMath.Vector3ClampMagnitude(v, maxSpeed);
        currentSpeed = VectorMath.Vector3Magnitude(velocity);
    }

    public void SetDirection(Vec3 dir)
    {
        Vec3 normalized = VectorMath.Vector3Normalize(dir);
        velocity = VectorMath.Vector3Scale(normalized, currentSpeed);
    }
}
