using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{

    [Header("Spawner Settings")]
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private DronePath pathToFollow;
    [SerializeField] private int maxDrones = 10;
    [SerializeField] private float respawnDelay = 5f;

    [Header("Initial Spawn")]
    [SerializeField] private float initialSpawnInterval = 1.5f;

    private List<GameObject> aliveDrones = new List<GameObject>();
    private bool isRespawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(InitialSpawnRoutine());
    }

    private void SpawnDrone()
    {
        if(dronePrefab ==  null || spawnPoint == null || pathToFollow == null)
        {
            Debug.LogWarning($"Spawner '{name}' is missing references.");
            return;
        }

        GameObject newDrone = Instantiate(dronePrefab, spawnPoint.position, spawnPoint.rotation);

        Health health = newDrone.GetComponent<Health>();
        if (health != null)
        {
            health.setSpawner(this);
        }

        DroneWaypointPatrol droneWaypointPatrol = newDrone.GetComponent<DroneWaypointPatrol>();
        if (droneWaypointPatrol != null)
        {
            droneWaypointPatrol.patrolPath = pathToFollow;
        }

        aliveDrones.Add(newDrone);
    }

    public void NotifyDroneDestroyed(GameObject drone)
    {
        Debug.Log("Notifed");
        if (aliveDrones.Contains(drone))
        {
            aliveDrones.Remove(drone);
        }

        if (!isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator InitialSpawnRoutine()
    {
        for (int i = 0; i < maxDrones; i++)
        {
            SpawnDrone();
            yield return new WaitForSeconds(initialSpawnInterval);
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        while (aliveDrones.Count < maxDrones)
        {
            yield return new WaitForSeconds(respawnDelay);
            SpawnDrone();
        }

        isRespawning = false;
    }


}
