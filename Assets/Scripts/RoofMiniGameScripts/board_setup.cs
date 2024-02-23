using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board_setup : MonoBehaviour
{
    
    public GameObject tilePrefab;

    public int columns = 15;
    public int rows = 10;

    public float tileSize = 0.5f;

    //adjust x and y position in unity as needed
    public float xPosition = 0f;
    public float yPosition = 0f;

    // Collider offset
    public float colliderOffset = 0.1f;

    void Start()
    {
        //called in start function for now - ideally will be called in gameplay manager script.
        BoardSetup();

        //add box colliders to the sides and bottom of the grid.
        AddBoxColliders();
    }

    //function to spawn board tiles - the startPosition is relative to the object the script is attached to.
    void BoardSetup()
    {
        Vector3 startPosition = new Vector3(xPosition, yPosition, 0);

        //this loop spawns the grid rows and columns.
        //it does this by calculating the tilePosition based on the row and column values,
        //then instantiates the prefab at tilePosition with no rotation and transform as the 
        //position of the game object this script is attached to.
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 tilePosition = startPosition + new Vector3(col * tileSize, row * tileSize, 0);
                Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
            }
        }
    }

    // Function to add box colliders and a rigidbody to the sides and bottom of the grid.
    
void AddBoxColliders()
{
    //new GameObject for each side collider.
    GameObject leftCollider = new GameObject("LeftCollider");
    GameObject rightCollider = new GameObject("RightCollider");
    GameObject bottomCollider = new GameObject("BottomCollider");

    //attach the colliders to the grid object as child objects.
    leftCollider.transform.parent = transform;
    rightCollider.transform.parent = transform;
    bottomCollider.transform.parent = transform;

    //set collider tags
    leftCollider.tag = "leftCollider";
    rightCollider.tag = "rightCollider";
    bottomCollider.tag = "yCollider";

    //calculate size for collider size using board size
    float colliderWidth = tileSize / 2f;
    float colliderHeight = rows * tileSize;
    float colliderDepth = 1f; 

    //left collider at left edge
    Vector3 leftColliderPosition = new Vector3(xPosition - colliderWidth - colliderOffset, yPosition + colliderHeight / 2f - tileSize / 2f, 0f);

    //right collider position at right edge
    Vector3 rightColliderPosition = new Vector3(xPosition + (columns - 1) * tileSize + colliderWidth + colliderOffset, yPosition + colliderHeight / 2f - tileSize / 2f, 0f);

    //bottom collider at bottom - slightly raised due to not stoping - will revisit
    Vector3 bottomColliderPosition = new Vector3(xPosition + (columns - 1) * tileSize / 2f, yPosition - colliderWidth / 2f - colliderOffset, 0f);

    //add collider, rigid body and set position 
    leftCollider.AddComponent<BoxCollider2D>().size = new Vector2(colliderWidth, colliderHeight);
    leftCollider.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    leftCollider.transform.position = leftColliderPosition;

    rightCollider.AddComponent<BoxCollider2D>().size = new Vector2(colliderWidth, colliderHeight);
    rightCollider.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    rightCollider.transform.position = rightColliderPosition;

    bottomCollider.AddComponent<BoxCollider2D>().size = new Vector2(columns * tileSize, colliderWidth);
    bottomCollider.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    bottomCollider.transform.position = bottomColliderPosition;
}

}
