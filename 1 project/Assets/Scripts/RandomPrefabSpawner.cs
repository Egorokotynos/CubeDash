
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Array to hold the prefabs
    public float spawnInterval = 30f; // Interval for spawning
    public float moveSpeed = 1f; // Speed at which the spawner moves
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime; // Increase timer

        // Check if it's time to spawn
        if (timer >= spawnInterval)
        {
            SpawnRandomPrefab(); // Call the function to spawn prefab
            timer = 0f; // Reset timer
        }

        // Move the spawner forward
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void SpawnRandomPrefab()
    {
        // Randomly select a prefab from the array
        GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];

        // Instantiate the randomly selected prefab at the spawner's position and rotation
        GameObject spawnedPrefab = Instantiate(randomPrefab, transform.position, transform.rotation);

        // Destroy the spawned prefab after 30 seconds
        Destroy(spawnedPrefab, 30f);
    }
}
