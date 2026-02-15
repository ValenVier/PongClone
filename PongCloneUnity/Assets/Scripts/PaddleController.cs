using System;
using DefaultNamespace;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;

    public KeyCode upKey;
    public KeyCode downKey;

    public float topLimit = 3f;
    public float bottomLimit = -4.5f;

    private void Awake()
    {
        Paddle paddle = GetComponent<Paddle>();
        paddle.ResetPosition();
    }

    void Update()
    {
        float move = 0f;

        if (Input.GetKey(upKey))
            move = 1f;

        if (Input.GetKey(downKey))
            move = -1f;

        Vec3 position = new Vec3(transform.position.x, transform.position.y, transform.position.z);
        Vec3 delta = new Vec3(0f, move * speed * Time.deltaTime, 0f);
        position = VectorMath.Vector3Add(position, delta);

        // limits
        //position.y = Mathf.Clamp(position.y, bottomLimit, topLimit);
        float halfHeight = transform.localScale.y * 0.5f;

        position.y = Mathf.Clamp(position.y, bottomLimit + halfHeight, topLimit - halfHeight);

        transform.position = new Vector3(position.x, position.y, position.z);
    }
}
