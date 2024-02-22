using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board_setup : MonoBehaviour
{
    
    public GameObject tilePrefab;

    public int columns = 15;
    public int rows = 10;

    public float tileSize = 0.5f;

    // Adjust x and y position in unity as needed
    public float xPosition = 0f;
    public float yPosition = 0f;

    // Collider offset
    public float colliderOffset = 0.1f;

    void Start()
    {
        // Called in start function for now - ideally will be called in gameplay manager script.
        BoardSetup();

        // Add box colliders to the sides and bottom of the grid.
        AddBoxColliders();
    }

    // Function to spawn board tiles - the startPosition is relative to the object the script is attached to.
    void BoardSetup()
    {
        Vector3 startPosition = new Vector3(xPosition, yPosition, 0);

        // This loop spawns the grid rows and columns.
        // It does this by calculating the tilePosition based on the row and column values,
        // then instantiates the prefab at tilePosition with no rotation and transform as the 
        // position of the game object this script is attached to.
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 tilePosition = startPosition + new Vector3(col * tileSize, row * tileSize, 0);
                Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
            }
        }
    }

    // Function to add box colliders to the sides and bottom of the grid.
    void AddBoxColliders()
    {
        // Create a new GameObject for each side collider.
        GameObject leftCollider = new GameObject("LeftCollider");
        GameObject rightCollider = new GameObject("RightCollider");
        GameObject bottomCollider = new GameObject("BottomCollider");


        leftCollider.tag = "xCollider";
        rightCollider.tag = "xCollider";
        bottomCollider.tag = "yCollider";

        // Attach the colliders to the grid object as child objects.
        leftCollider.transform.parent = transform;
        rightCollider.transform.parent = transform;
        bottomCollider.transform.parent = transform;

        // Calculate positions for the colliders with offset to start outside the grid.
        float colliderWidth = tileSize / 2f;
        float colliderHeight = rows * tileSize;
        float colliderDepth = 1f; // Depth of the colliders (in Unity units).

        // Left collider position is at the leftmost edge of the grid minus collider offset.
        Vector3 leftColliderPosition = new Vector3(xPosition - colliderWidth - colliderOffset, yPosition + colliderHeight / 2f - tileSize / 2f, 0f);

        // Right collider position is at the rightmost edge of the grid plus collider offset.
        Vector3 rightColliderPosition = new Vector3(xPosition + (columns - 1) * tileSize + colliderWidth + colliderOffset, yPosition + colliderHeight / 2f - tileSize / 2f, 0f);

        // Bottom collider position is at the bottom edge of the grid minus collider offset.
        Vector3 bottomColliderPosition = new Vector3(xPosition + (columns - 1) * tileSize / 2f, yPosition - colliderWidth / 2f - colliderOffset, 0f);
        // Add Box Colliders to the collider GameObjects.
        leftCollider.AddComponent<BoxCollider>().size = new Vector3(colliderWidth, colliderHeight, colliderDepth);
        leftCollider.transform.position = leftColliderPosition;

        rightCollider.AddComponent<BoxCollider>().size = new Vector3(colliderWidth, colliderHeight, colliderDepth);
        rightCollider.transform.position = rightColliderPosition;

        bottomCollider.AddComponent<BoxCollider>().size = new Vector3(columns * tileSize, colliderWidth, colliderDepth);
        bottomCollider.transform.position = bottomColliderPosition;
    }
}
