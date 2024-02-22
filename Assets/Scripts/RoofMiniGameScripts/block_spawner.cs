using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_spawner : MonoBehaviour
{
    public GameObject[] shapePrefabs;
    //spawn position will be set when testing
    public Vector3 spawnPosition;



    //called at start() just for testing. 
    void Start()
    {
        SpawnBlock();
    }


    public void SpawnBlock()
    {
        //assign index from prefabs list(set in unity inspector)
        int shapeIndex = Random.Range(0, shapePrefabs.Length);
        GameObject shape = shapePrefabs[shapeIndex];


        /* The offset for spawning each shape is hard coded.
        Not an ideal solution but works(for now) as long as the shapePrefabs array is
        populated in a particular order(in the unity editor).

        ////When the script is attached to an object, set size to 4, element 0 = 2x2 cube,
        element 1 = straight line, element 2 = right L shape, element 3 = left L shape.

        */

        if(shapeIndex == 0)
        {
            //cube
            Debug.Log("shape 0");
            spawnPosition[1] = spawnPosition[1] - 0.25f;
            spawnPosition[0] = spawnPosition[0] - 0.0f;
            Instantiate(shape, spawnPosition, Quaternion.identity);
        }
        else if(shapeIndex == 1)
        {
            //line
            Debug.Log("shape 1");
            spawnPosition[1] = spawnPosition[1] - 0.25f;
            spawnPosition[0] = spawnPosition[0] - 0.25f;
            Instantiate(shape, spawnPosition, Quaternion.identity);
        }
        else if(shapeIndex == 2)
        {
            //Right L
            Debug.Log("shape 2");
            Instantiate(shape, spawnPosition, Quaternion.identity);
        }
        else if(shapeIndex == 3)
        {
            //Left L
            Debug.Log("shape 3");
            Instantiate(shape, spawnPosition, Quaternion.identity);
        }
        

    }

}
