using UnityEngine;

public class block_controller : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float fallInterval = 1.0f;
    private float lastFallTime;
    public float minX = -3.0f;
    public float maxX = 4.0f;
    public float minY = -1.0f;
    public float maxY = 3.75f;

    void Start()
    {
        lastFallTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateBlock();
        }

        if (Time.time - lastFallTime >= fallInterval)
        {
            MoveDown();
        }
    }

    void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed;
        ClampPosition();
    }

    void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed;
        ClampPosition();
    }

    void MoveDown()
    {
        transform.position += Vector3.down * moveSpeed;
        lastFallTime = Time.time;
        ClampPosition();
    }

    void RotateBlock()
    {
        transform.Rotate(0f, 0f, 90f);
        ClampPosition();
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if the block is close to the bottom collider's position
        if (collision.gameObject.CompareTag("yCollider"))
        {
            // Stop the block's downward movement
            lastFallTime = Time.time;
        }

        // Check if the block is close to a side collider's position
        if (collision.gameObject.CompareTag("xCollider"))
        {
            ClampPosition();
        }
    }

    void ClampPosition()
    {
        // Clamp the block's position to stay within the bounds of the stage
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
        Debug.Log("Original Position: " + transform.position);
        Debug.Log("Clamped Position: " + clampedPosition);
    }
}
