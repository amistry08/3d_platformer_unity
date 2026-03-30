using UnityEngine;

public class DroneNotifyDied : MonoBehaviour
{
    private DroneSpawner spawner;

    private void Update()
    {
        if (gameObject.GetComponent<Health>().currentHealth == 0)
        {
            Debug.Log("Health " + gameObject.GetComponent<Health>().currentHealth);
            if(spawner != null)
            {
                spawner.NotifyDroneDestroyed(gameObject);
            }
        }
    }

    public void setSpawner(DroneSpawner spawner)
    {
        this.spawner = spawner;
    }

}
