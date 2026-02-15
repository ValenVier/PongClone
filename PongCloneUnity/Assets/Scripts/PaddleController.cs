using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;

    public KeyCode upKey;
    public KeyCode downKey;

    public float topLimit = 3f;
    public float bottomLimit = -4.5f;

    void Update()
    {
        float move = 0f;

        if (Input.GetKey(upKey))
            move = 1f;

        if (Input.GetKey(downKey))
            move = -1f;

        Vector3 position = transform.position;

        position.y += move * speed * Time.deltaTime;

        // limits
        //position.y = Mathf.Clamp(position.y, bottomLimit, topLimit);
        float halfHeight = transform.localScale.y * 0.5f;

        position.y = Mathf.Clamp(position.y, bottomLimit + halfHeight, topLimit - halfHeight);

        transform.position = position;
    }
}
