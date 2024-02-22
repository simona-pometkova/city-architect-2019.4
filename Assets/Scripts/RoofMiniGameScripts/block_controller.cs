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

    //bool to stop blocks when they reach the bottom
    private bool canMove = true;

    void Start()
    {
        lastFallTime = Time.time;
    }

    void Update()
        {
            //check if can move
            if (!canMove)
                return;

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

    //basic movement
    void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed;
    }

    void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed;
        
    }

    void MoveDown()
    {
        transform.position += Vector3.down * moveSpeed;
        lastFallTime = Time.time;
        
    }

    void RotateBlock()
    {
        


        //Rotating objects get displaced by 0.5 units except the cube(2x2)
        //this workaround removes the misalignment but moves blocks to the left slightly
        //on rotation. Will come back to address if time allows.

        //if 2x2 don't place offset
        if(gameObject.name == "2x2_single(Clone)")
        {
            transform.Rotate(0f, 0f, 90f);
        }
        else
        {
            //otherwise offset by 0.25
            transform.Rotate(0f, 0f, 90f);
            transform.position += new Vector3(-0.25f, -0.25f, 0f);
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check for collisions
        if (collision.gameObject.CompareTag("xCollider"))
        {
             Debug.Log("Collision on X Axis");

        }
        if (collision.gameObject.CompareTag("yCollider"))
        {
            //if collides with bottom, trigger canMove to false
             Debug.Log("Collision on Y Axis");
             canMove = false;

        }
    }


}
