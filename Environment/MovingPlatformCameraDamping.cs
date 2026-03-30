using UnityEngine;
using Unity.Cinemachine;

public class MovingPlatformCameraDamping : MonoBehaviour
{

    [Header("Settings")]
    public CinemachineCamera cinemachineCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && cinemachineCamera != null)
        {
           cinemachineCamera.GetComponent<CinemachineRotationComposer>().Damping = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && cinemachineCamera != null)
        {
            cinemachineCamera.GetComponent<CinemachineRotationComposer>().Damping = new Vector2(0.5f, 0.5f);
        }
    }
}
