using UnityEngine;

public class DronePath : MonoBehaviour
{
    [SerializeField] 
    private Transform[] waypoints;

    public Transform[] Waypoints => waypoints;
  
    private void OnDrawGizmos()
    {

        if (waypoints ==  null || waypoints.Length == 0) return;

        Gizmos.color = Color.white;

        for (int i = 0; i < waypoints.Length ; i++)
        {
            if (waypoints[i] == null)
            { 
                continue;
            }

            //Draw point
            Gizmos.DrawSphere(waypoints[i].position, 0.3f);

            //Draw line to next point
            if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }

        }

        if (waypoints.Length > 1)
        {
            Gizmos.DrawLine(
                waypoints[waypoints.Length - 1].position,
                waypoints[0].position
            );
        }
    }

}
