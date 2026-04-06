using UnityEngine;

public class PortalTrigger : MonoBehaviour
{

    public bool needCoreParts = false;
    public GameObject portalSurface;
    public Material activeMaterial;
    public Material inActiveMaterial;

    public string playerPrefKey;

    private bool isActive = false;

    private void Start()
    {   
        if (needCoreParts)
        {
            DeactivatePortal();
            portalSurface.GetComponent<BoxCollider>().enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!needCoreParts) return;

        isActive = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;
        if (other.tag == "Player" && isActive)
        {
            portalSurface.GetComponent<BoxCollider>().enabled = true;
            portalSurface.GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else
        {
            DeactivatePortal();
        }
    }

    public void DeactivatePortal()
    {
        Debug.Log("DeactivatePortal");
        isActive = false;
        portalSurface.GetComponent<MeshRenderer>().material = inActiveMaterial;
    }

}
