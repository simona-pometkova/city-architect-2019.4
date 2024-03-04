using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlockController : MonoBehaviour
{
    //set timer variables for blocks falling. 
    private float lastFallTime;
    public float fallTime = 0.8f;
    //Set boundry for grid setup - if you change the grid size in the editer these must be changed too
    public static int boundHeight = 12;
    public static int boundWidth = 15;
    //add a rotation point for the blocks so they line up with the grid
    public Vector3 rotationPoint;
    //declare grid to store positions of placed blocks
    public static Transform[,] grid = new Transform[boundWidth, boundHeight];


    //gameover flag
    public static bool gameOver = false;
    






    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //basic movement controls - may need to be changed later down the line
        
        if(gameOver == true)
        {
            //Debug.Log("gameOver=true");
        }
        else
        {
            
            CheckEndGame();
            //move left
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //move left when pressed
                transform.position += new Vector3(-1,0,0);
                //if move is not allowed undo the transform
                if(ValidateMove() == false)
                {
                    transform.position -= new Vector3(-1,0,0);
                }
            }
            //same as above just for the right arrow
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1,0,0);
                if(ValidateMove() == false)
                {
                    transform.position -= new Vector3(1,0,0);
                }
            }
            //If the down key is pressed - divide falltime by 10 while the key is held so the block drops faster
            if(Time.time - lastFallTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0,-1,0);
                if(ValidateMove() == false && gameOver == false)
                {
                    transform.position += new Vector3(0,1,0);
                    //update the grid with newly placed block
                    UpdateGrid();
                    //disable block this is attached to
                    this.enabled = false;
                    //spawn new block
                    FindObjectOfType<BlockSpawner>().SpawnBlock();
                    
                }
                //update time
                lastFallTime = Time.time;
            }
            //block rotation
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //convert local rotation to global
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
                if(ValidateMove() == false)
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
                }
            }
        }

    }
    bool ValidateMove()
    {
        //validate movement based on the stage boundary
        //this loop goes over each transform in the gameobject - essentially the smaller tiles which the blocks are made up of
        foreach(Transform children in transform)
        {
            //position rounded to int only - each block is made of smaller tiles which are 1 unity block
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            //check if outside bounds
            if(roundX < 0 || roundX >= boundWidth || roundY < 0 || roundY >= boundHeight)
            {
                return false;
            }
            if(grid[roundX,roundY] != null)
            {
                return false;
            }
        }
        return true;
    }
    void UpdateGrid()
    {
        //for each tile in block
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            //add to grid
            grid[roundX, roundY] = children;
        }
    }



    //check end game
    public void CheckEndGame()
    {
        //check transform grid for objects in the top row
        for (int x = 0; x < boundWidth; x++)
        {
            if (grid[x, boundHeight-2] != null)
            {
                //ADD DISPLAY MESSAGE FUNCTION HERE
                //Debug.Log("Call CheckEndGame");
                gameOver = true;

                //END GAME SCRIPT TRANSITION GOES HERE
            }
            else
            {
                //Debug.Log("No Transform object found at position (" + x + ", " + (boundHeight - 1) + ")");
            }
        }
    }
    public static Transform[,] GetGrid()
    {
        return grid;
    }
    public static void ResetGrid()
    {
        grid = new Transform[boundWidth,boundHeight];
    }


}
