using UnityEngine;
using Unity.Cinemachine;

public class MovingPlatformCamera : MonoBehaviour
{
    [Header("Settings")]
    public CinemachineCamera cinemachineCamera;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cinemachineCamera.Priority = 20;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cinemachineCamera.Priority = -1;
        }
    }

}
