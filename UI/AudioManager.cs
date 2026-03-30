using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioClip exploration;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        playMusic(exploration);
    }

    public void playMusic(AudioClip clip) 
    {
        if (musicSource.clip == clip) return;

        StopAllCoroutines();
        StartCoroutine(FadeToNewTrack(clip));
    }

    IEnumerator FadeToNewTrack(AudioClip newClip)
    {
        //Fade Out
        while (musicSource.volume> 0)
        {
            musicSource.volume -= Time.deltaTime;
            yield return null;
        }

        musicSource.clip= newClip;
        musicSource.Play();

        //Fade In
        while(musicSource.volume < 1)
        {
            musicSource.volume += Time.deltaTime;
            yield return null;
        }

    }

}