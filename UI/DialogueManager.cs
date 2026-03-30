using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcLinesText;

    [Header("Input Actions & Controls")]
    public InputAction interractAction;

    [Header("Cinemachine Camera")]
    public CinemachineCamera dialogueCamera;

    private AudioSource audioSource;

    private Dialogue currentDialogue;
    private Objective currentObjective;
    private int currentLineIndex;
    private bool isDialogueActive = false;
    private float advanceLockTimer = 0f;

    private NpcDialogueTrigger currentNpc;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialoguePanel.SetActive(false);
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(advanceLockTimer > 0f)
        {
            advanceLockTimer -= Time.unscaledDeltaTime; ;
        }

        if (advanceLockTimer > 0f) return;

        if (!isDialogueActive) return;



        if(interractAction.WasPerformedThisFrame()) 
        {
 
            PlayInteractSound();
            ShowNextLine();
        }
    }

    private void ShowNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            npcLinesText.text = currentDialogue.lines[currentLineIndex];
        }
        else
        {
            EndDialogue();
            ObjectiveManager.Instance.SetObjective(currentObjective);
        }
    }

    private void EndDialogue()
    {
        if (dialogueCamera != null)
        {
            dialogueCamera.Priority = 0;
        }

        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        Time.timeScale = 1f;

    }

    public void StartDialogue(Dialogue dialogue,Objective objective,  NpcDialogueTrigger npc)
    {
        currentDialogue = dialogue;
        currentObjective = objective;
        currentNpc = npc;
        currentLineIndex = 0;
        isDialogueActive = true;

        advanceLockTimer = 0.15f;

        if (dialogueCamera != null && npc.dialogueCamAnchor != null)
        {
            dialogueCamera.transform.position = npc.dialogueCamAnchor.position;
            dialogueCamera.transform.rotation = npc.dialogueCamAnchor.rotation;
            dialogueCamera.Priority = 20;
        }

        dialoguePanel.SetActive(true);
        npcNameText.text = currentDialogue.npcName;
        npcLinesText.text = currentDialogue.lines[currentLineIndex];

        Time.timeScale = 0f;
    }
    public void PlayInteractSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    /// <summary>
    /// Standard Unity function called whenever the attached gameobject is enabled
    /// </summary>
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
