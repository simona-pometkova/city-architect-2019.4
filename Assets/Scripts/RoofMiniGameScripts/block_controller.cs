using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_controller : MonoBehaviour
{
    //variables to adjust movement speed and fall interval of blocks

    //each block is 0.5f units so move 1 block
    public float moveSpeed = 0.5f; 
    //set time for fall interval(in seconds)
    public float fallInterval = 1.0f; 
    //declare last interval
    private float lastFallTime;

    void Start()
    {
        //initialise lastFallTime
        lastFallTime = Time.time;
    }


    void Update()
    {
        //Basic movement controls - will have to be changed depending on what input system package we use
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed);
        }


        if (Time.time - lastFallTime >= fallInterval)
        {
            //multiply vertical movement by 0.5 as each block is half unity unit
            transform.Translate(Vector3.down * 0.5f);
            //update lastFallTime
            lastFallTime = Time.time;
        }
    }
}
