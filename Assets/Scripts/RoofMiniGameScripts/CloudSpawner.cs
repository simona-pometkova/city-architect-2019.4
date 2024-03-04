using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    public int maxClouds = 5;
    public float spawnInterval = 15f;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    
    public float moveSpeed = 0.1f;
    public float despawnX = -15f; // Or any offscreen value to determine when to despawn clouds

    private float spawnTimer;
    private int currentClouds;

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        MoveClouds();
        if (spawnTimer <= 0f && currentClouds < maxClouds)
        {
            SpawnCloud();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnCloud()
    {
        int cloudIndex = Random.Range(0, cloudPrefabs.Length);
        GameObject cloud = Instantiate(cloudPrefabs[cloudIndex]);
        float spawnHeight = Random.Range(0,10);
        float scale = Random.Range(minScale, maxScale);
        cloud.transform.localScale = new Vector3(scale, scale, 1f);

        float randomX = Random.Range(-10f, 10f); // Spawn within a range of X positions
        cloud.transform.position = new Vector3(30f, spawnHeight, 0f);

        currentClouds++;
    }

    void FixedUpdate()
    {
        MoveClouds();
        DespawnClouds();
    }

    void MoveClouds()
    {
        foreach (GameObject cloud in GameObject.FindGameObjectsWithTag("Cloud"))
        {
            cloud.transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime); // Move left
        }
    }

    void DespawnClouds()
    {
        foreach (GameObject cloud in GameObject.FindGameObjectsWithTag("Cloud"))
        {
            if (cloud.transform.position.x <= despawnX)
            {
                Destroy(cloud);
                currentClouds--;
            }
        }
    }
}
