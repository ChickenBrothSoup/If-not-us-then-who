using UnityEngine;
using System.Collections;


public class MonsterIconSpawnerScript : MonoBehaviour
{
    public GameObject iconPrefab;
    public Transform topPoint;
    public Transform bottomPoint;
    public int iconCount = 5;
    public float waitBeforeSpawning = 3f;
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 2f;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        // Wait before spawning starts
        yield return new WaitForSeconds(waitBeforeSpawning);

        for (int i = 0; i < iconCount; i++)
        {
            float randomT = Random.Range(0f, 1f);
            Vector3 spawnPos = Vector3.Lerp(topPoint.position, bottomPoint.position, randomT);

            GameObject icon = Instantiate(iconPrefab, spawnPos, Quaternion.identity);
            

            // Random interval between each spawn
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }
}
