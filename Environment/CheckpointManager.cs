using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    private List<Checkpoint> activatedCheckpoints = new List<Checkpoint>();

    private void Awake()
    {
        if(instance == null) instance= this;
        else Destroy(gameObject);
    }

    public void RegisterCheckpoint(Checkpoint checkpoint)
    {
        if(!activatedCheckpoints.Contains(checkpoint))
        {
            activatedCheckpoints.Add(checkpoint);
        }
    }

    public Transform GetClosestActivatedCheckpoint(Vector3 position)
    {
        if(activatedCheckpoints.Count== 0) return null;

        Checkpoint closest = null;
        float closestDistance = Mathf.Infinity;

        foreach(Checkpoint checkpoint in activatedCheckpoints)
        {
            float distance = Vector3.Distance(position, checkpoint.respawnLocation.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closest = checkpoint;
            }
        }

        return closest != null ? closest.respawnLocation : null;
    }
}

