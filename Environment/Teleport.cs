using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The Destination object with a Teleport script Attached")]
    public Teleport destinationTeleporter;

    [Tooltip("Effect when Tekpoerting")]
    public GameObject teleportEffect;


    private bool teleporterAvailable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && teleporterAvailable && destinationTeleporter != null)
        {
            if(teleportEffect != null)
            {
                Instantiate(teleportEffect, transform.position, transform.rotation, null);
            }

            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }
            
            teleporterAvailable = false;

            float heightOffset = transform.position.y - other.transform.position.y;
            other.transform.position = destinationTeleporter.transform.position - new Vector3(0, heightOffset, 0);

            if (characterController != null)
            {
                characterController.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            teleporterAvailable = true;
        }
    }



}
