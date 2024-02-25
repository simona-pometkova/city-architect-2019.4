using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Blocks;
    void Start()
    {
        SpawnBlock();
    }

    // Update is called once per frame
    public void SpawnBlock()
    {
        Instantiate(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
    }
}
