using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] private FodderMovementScript enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform playerSideTarget;

    [Header("Spawn Settings")]
    [SerializeField] private float firstSpawnDelay = 1f;
    [SerializeField] private float spawnInterval = 2f;

    [Tooltip("Random spawn distance from the Spawn Point on the X and Y axes.")]
    [SerializeField] private Vector2 spawnRange = new Vector2(5f, 3f);

    private Coroutine spawnCoroutine;

    private void Start()
    {
        if (!ValidateReferences())
        {
            enabled = false;
            return;
        }

        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();

        FodderMovementScript newEnemy = Instantiate(
            enemyPrefab,
            randomSpawnPosition,
            Quaternion.identity
        );

        newEnemy.SetTarget(playerSideTarget);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnRange.x, spawnRange.x);
        float randomY = Random.Range(-spawnRange.y, spawnRange.y);

        return spawnPoint.position + new Vector3(randomX, randomY, 0f);
    }

    private bool ValidateReferences()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError(
                "EnemySpawner: Enemy Prefab has not been assigned.",
                this
            );

            return false;
        }

        if (spawnPoint == null)
        {
            Debug.LogError(
                "EnemySpawner: Spawn Point has not been assigned.",
                this
            );

            return false;
        }

        if (playerSideTarget == null)
        {
            Debug.LogError(
                "EnemySpawner: Player Side Target has not been assigned.",
                this
            );

            return false;
        }

        if (spawnInterval <= 0f)
        {
            Debug.LogError(
                "EnemySpawner: Spawn Interval must be greater than zero.",
                this
            );

            return false;
        }

        if (firstSpawnDelay < 0f)
        {
            Debug.LogError(
                "EnemySpawner: First Spawn Delay cannot be negative.",
                this
            );

            return false;
        }

        if (spawnRange.x < 0f || spawnRange.y < 0f)
        {
            Debug.LogError(
                "EnemySpawner: Spawn Range values cannot be negative.",
                this
            );

            return false;
        }

        return true;
    }

    private void OnDisable()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Shows the spawning area in the Unity Scene view.
    private void OnDrawGizmosSelected()
    {
        if (spawnPoint == null)
        {
            return;
        }

        Gizmos.DrawWireCube(
            spawnPoint.position,
            new Vector3(spawnRange.x * 2f, spawnRange.y * 2f, 0f)
        );
    }
}
