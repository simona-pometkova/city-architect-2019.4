using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    //declare gameobject array - populate with blocks in unity editor
    public GameObject[] Blocks;
    void Start()
    {
        //spawn a block at the start of the game
        SpawnBlock();
    }

    // Update is called once per frame
    public void SpawnBlock()
    {   //instantiate random block from the Blocks Array at the position of the object this script is attached to with no rotation
        Instantiate(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
    }
}
