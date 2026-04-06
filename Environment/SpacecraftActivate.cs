using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class SpacecraftActivate : MonoBehaviour

{
    public GameObject player;

    public GameObject interractHint;

    [Header("Effects")]
    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;
    public ParticleSystem explosionEffect;
    public ParticleSystem flashEffect;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip explosionClip;
    public AudioClip launchClip;
    public AudioClip engineClip;


    [Header("Movement")]
    public float launchDuration = 4f;
    public float moveUpSpeed = 8f;


    [Header("Cinemachine Camera")]
    public CinemachineCamera shipCamera;

    [Header("Input Actions & Controls")]
    public InputAction interractAction;

    private bool playerInRange = false;

    void Start()
    {
        if (interractHint != null)
        {
            interractHint.SetActive(false);
        }
        if (shipCamera != null)
        {
            shipCamera.Priority = 0;
        }
        // Start smoke / fire
        if (smokeEffect != null) smokeEffect.Stop();
        if (explosionEffect != null) fireEffect.Stop();
        if (flashEffect != null) explosionEffect.Stop();
        if (flashEffect != null) flashEffect.Stop();

    }

    void Update()
    {

        if (playerInRange && PlayerPrefs.GetInt("Flag_HasAllParts", 0) == 1)
        {
            if (interractAction.WasPerformedThisFrame())
            {
                StartEndSequence(); 
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            playerInRange = true;
            if (interractHint != null)
            {
                interractHint.SetActive(true);
            }
            if (shipCamera != null)
            {
                shipCamera.Priority = 10;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            if (interractHint != null)
            {
                interractHint.SetActive(false);
            }
            if (shipCamera != null)
            {
                shipCamera.Priority = 0;
            }
        }
    }

    public void StartEndSequence()
    {
        StartCoroutine(PlayEndSequence());
    }

    IEnumerator PlayEndSequence()
    {
        if (interractHint != null)
        {
            interractHint.SetActive(false);
        }

        if(player != null)
        {
            player.SetActive(false);
        }

        // Start smoke / fire
        if (smokeEffect != null) smokeEffect.Play();
        if (explosionEffect != null) explosionEffect.Play();
        if (flashEffect != null) flashEffect.Play();

        if (audioSource != null && explosionClip != null)
            audioSource.PlayOneShot(explosionClip);

        // Small delay before blastoff
        yield return new WaitForSeconds(2f);

        if (smokeEffect != null) smokeEffect.Stop();
        if (fireEffect != null) fireEffect.Play();

        if (audioSource != null && launchClip != null)
            audioSource.PlayOneShot(launchClip);

        // Small delay before blastoff
        yield return new WaitForSeconds(3f);

        if (audioSource != null && explosionClip != null)
            audioSource.PlayOneShot(explosionClip);
       
        if (audioSource != null && launchClip != null)
            audioSource.PlayOneShot(engineClip);

        float timer = 0f;

        while (timer < launchDuration)
        {
            if (shipCamera != null)
            {
                shipCamera.Priority = 10;
            }
            timer += Time.deltaTime;

            if (gameObject != null)
            {
                gameObject.transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;
            }

            yield return null;
        }

       


        yield return new WaitForSeconds(1f);

        if (GameManager.instance != null)
        {
            Destroy(this.gameObject);
            GameManager.instance.LevelCleared();

        }
    }

    private void OnEnable()
    {
        interractAction.Enable();
    }

    /// <summary>
    /// Standard Unity function called whenever the attached gameobject is disabled
    /// </summary>
    private void OnDisable()
    {
        interractAction.Disable();
    }
}
