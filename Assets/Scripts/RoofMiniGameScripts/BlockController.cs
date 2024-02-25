using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private float lastFallTime;
    public float fallTime = 0.8f;
    public static int boundHeight = 15;
    public static int boundWidth = 10;
    public Vector3 rotationPoint;
    private static Transform[,] grid = new Transform[boundWidth, boundHeight];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1,0,0);
            if(ValidateMove() == false)
            {
                transform.position -= new Vector3(-1,0,0);
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1,0,0);
            if(ValidateMove() == false)
            {
                transform.position -= new Vector3(1,0,0);
            }
        }
        if(Time.time - lastFallTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0,-1,0);
            if(ValidateMove() == false)
            {
                transform.position += new Vector3(0,1,0);
                UpdateGrid();
                this.enabled = false;
                FindObjectOfType<BlockSpawner>().SpawnBlock();
            }
            lastFallTime = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if(ValidateMove() == false)
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }
    }
    bool ValidateMove()
    {
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

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
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundX, roundY] = children;
        }
    }

}
