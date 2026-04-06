using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyZoneSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyEntry
    {
        public GameObject enemyPrefab;
        [Range(0f, 100f)] public float weight = 1f;
    }

    [Header("Spawn Settings")]
    public EnemyEntry[] enemyTypes;
    public Transform[] spawnPoints;
    public int maxAliveEnemies = 5;
    public float respawnDelay = 5f;

    [Header("Spawn Rules")]
    public float minDistanceFromPlayer = 15f;
    public Transform player;

    private List<GameObject> aliveEnemies = new List<GameObject>();
    private Transform spawnedEnemies;

    private void Awake()
    {
        GameObject container = new GameObject("SpawnedEnemies");
        container.transform.parent = this.transform;
        container.transform.localPosition = Vector3.zero;
        spawnedEnemies = container.transform;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FillZoneAtStart());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (aliveEnemies.Count < maxAliveEnemies)
        {
            SpawnEnemy();
        }
    }

    private IEnumerator FillZoneAtStart()
    {
        yield return null;
        while (aliveEnemies.Count < maxAliveEnemies)
        {
            yield return respawnDelay;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject chosenPrefab = GetRandomEnemyPrefab();
        Transform chosenPoint = GetRandomSpawnPoint();

        if(chosenPrefab == null || chosenPrefab == null)
        {
            Debug.LogWarning("EnemyZoneSpawner: No enemy prefab or spawn point found.");
            return;
        }

        Vector3 spawnPosition = chosenPoint.position;

        if (!TryGetValidNavMeshPosition(spawnPosition, out Vector3 navPos))
        {
            Debug.LogWarning("Could not find valid NavMesh position near spawn point.", chosenPoint);
            return;
        }

        if (player != null && Vector3.Distance(player.position, navPos) < minDistanceFromPlayer)
        {
            Transform farPoint = GetRandomFarSpawnPoint();
            if (farPoint != null && TryGetValidNavMeshPosition(farPoint.position, out Vector3 farNavPos))
            {
                navPos = farNavPos;
            }
        }

        GameObject newEnemy = Instantiate(chosenPrefab, navPos, Quaternion.identity, spawnedEnemies);
        Health enemyHealth = newEnemy.GetComponent<Health>();
        if(enemyHealth != null)
        {
            enemyHealth.setZoneSpawner(this);
            aliveEnemies.Add(newEnemy);
        }
        else
        {
            Debug.LogWarning($"Enemy prefab {chosenPrefab.name} is missing EnemyHealth.", chosenPrefab);
        }


    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyTypes == null || enemyTypes.Length == 0) return null;
        float totalWeight = 0f;
        foreach (EnemyEntry entry in enemyTypes)
        {
            if(entry.enemyPrefab != null)
            {
                totalWeight += entry.weight;
            }
        }

        if (totalWeight <= 0f) return null;

        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (EnemyEntry entry in enemyTypes)
        {
            if (entry.enemyPrefab == null) continue;

            currentWeight += entry.weight;
            if (randomValue <= currentWeight)
            {
                return entry.enemyPrefab;
            }
        }

        return enemyTypes[0].enemyPrefab;
    }

    private Transform GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return null;
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];

    }

    private Transform GetRandomFarSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Length == 0 || player == null) return null;

        List<Transform> validPoints = new List<Transform>();
        foreach (Transform point in spawnPoints)
        {
            if (Vector3.Distance(player.position, point.position) >= minDistanceFromPlayer)
            {
                validPoints.Add(point);
            }
        }

        if (validPoints.Count == 0) return GetRandomSpawnPoint();

        int randomIndex = Random.Range(0, validPoints.Count);
        return validPoints[randomIndex];
    }

    public void NotifyEnemyDied(GameObject deadEnemy)
    {
        if (aliveEnemies.Contains(deadEnemy))
        {
            aliveEnemies.Remove(deadEnemy);
        }

        StartCoroutine(RespawnAfterDelay());
    }

    private bool TryGetValidNavMeshPosition(Vector3 sourcePosition, out Vector3 result)
    {
        if (NavMesh.SamplePosition(sourcePosition, out NavMeshHit hit, 4f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = sourcePosition;
        return false;
    } 
}

