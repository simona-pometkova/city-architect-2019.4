using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board_setup : MonoBehaviour
{
    //assign prefeb from unity interface.
    public GameObject tilePrefab;

    public int columns = 15;
    public int rows = 10;

    public float tileSize = 0.5f;
    
    //adjust x and y position in unity editor as needed.
    public float xPosition = 0f;
    public float yPosition = 0f;


    void Start()
    {
        //Called in start function for now - ideally will be called in gameplay manager script
        BoardSetup();
    }
    //function to spawn board tiles - the startPosition is relative to the object the script is attached to.
    void BoardSetup()
    {   
        Vector3 startPosition = new Vector3(xPosition, yPosition, 0);


        /* This loop spawns the grid rows and columns.
        It does this by calulation the tilePosition based on the upon the row and column values
        it then instantiates the prefab, in tilePosition with no rotation and transform is the 
        position of the game object this script is attached to */
        
        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < columns; col++)
            {
                Vector3 tilePosition = startPosition + new Vector3(col, row, 0) * tileSize;
                Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
            }
        }

    }
}
