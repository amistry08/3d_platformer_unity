using UnityEngine;

public class MusicZoneTrigger : MonoBehaviour
{
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AudioManager.Instance.playMusic(clip);
        }
    }
}
