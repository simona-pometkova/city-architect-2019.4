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

        Instantiate(shape, spawnPosition, Quaternion.identity);
    }

}
