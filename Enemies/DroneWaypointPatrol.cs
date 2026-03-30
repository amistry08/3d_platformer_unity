using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DroneWaypointPatrol : MonoBehaviour
{
    public DronePath patrolPath;
    
    [SerializeField]
    private float waitTimeAtPoint = 2f;

    private NavMeshAgent agent;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float waitTimer;
    private bool isWaiting;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        if (patrolPath == null || patrolPath.Waypoints == null || patrolPath.Waypoints.Length == 0)
        {
            Debug.LogWarning($"{name}: Patrol path is missing or empty.");
            return;
        }
        waypoints = patrolPath.Waypoints;
        MoveToWaypoint();
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        
        if (agent.pathPending) return;

        if(!isWaiting && agent.remainingDistance <= agent.stoppingDistance)
        {
            isWaiting = true;
            waitTimer = waitTimeAtPoint;
        }

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0f)
            {
                isWaiting=false;
                GoToNextWaypoint();
            }
        }
    }

    private void GoToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        MoveToWaypoint();
    }

    public void MoveToWaypoint()
    {
        if (waypoints[currentWaypointIndex] != null)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

}
