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

    private Rigidbody2D rb;

    //global bools to stop movement...i know i know...globals flags are bad..
    private bool canMove = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    void Start()
    {
        lastFallTime = Time.time;
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
        {

            //check if block movement is allowed
            if (!canMove)
                return;

            //move left if the left arrow key is pressed and movement to the left is allowed
            if (Input.GetKeyDown(KeyCode.LeftArrow) && canMoveLeft)
            {
                MoveLeft();
            }
            //move right if the right arrow key is pressed and movement to the right is allowed
            else if (Input.GetKeyDown(KeyCode.RightArrow) && canMoveRight)
            {
                MoveRight();
            }
            //rotate the block if the space key is pressed
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                RotateBlock();
            }

            //move the block down at regular intervals
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
        if (collision.gameObject.CompareTag("leftCollider"))
            {
                Debug.Log("Collision on left");
                canMoveLeft = false;
            }
        if (collision.gameObject.CompareTag("rightCollider"))
            {
                Debug.Log("Collision on right");
                canMoveRight = false;
            }
        if (collision.gameObject.CompareTag("yCollider"))
        {
            //if collides with bottom, trigger canMove to false
             Debug.Log("Collision on Y Axis");
             canMove = false;

        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //check for collisions
        if (collision.gameObject.CompareTag("leftCollider"))
        {
            Debug.Log("Collision exit on left");
            canMoveLeft = true;
        }
        if (collision.gameObject.CompareTag("rightCollider"))
        {
            Debug.Log("Collision exit on right");
            canMoveRight = true;
        }
    }


}
