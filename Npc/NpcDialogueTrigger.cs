using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class NpcDialogueTrigger : MonoBehaviour
{
    public GameObject interractHint;

    [Header("Dialogue")]
    public Dialogue dialogue;
    public Dialogue taskCompletedDailogue;

    [Header("Objective")]
    public Objective objective;

    [Header("Dialogue key flag")]
    public string requiredFlag = "";

    [Header("Input Actions & Controls")]
    public InputAction interractAction;

    [Header("Camera Positioning")]
    public Transform dialogueCamAnchor;

    private bool playerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(interractHint != null)
        {
            interractHint.SetActive(false);
        }
        if(objective == null) 
        {
            objective.showObjective= false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (playerInRange && !DialogueManager.instance.IsDialogueActive())
        {

            if (dialogue == null) return;
           
            if (interractAction.WasPerformedThisFrame() )
            {

                InitiatePlayerPrefActivateFunction();
                DialogueManager.instance.PlayInteractSound();

                Dialogue currentDialogue = dialogue;
                if (IsTaskCompleted())
                {
                    currentDialogue = taskCompletedDailogue;
                    objective.showObjective = false;
                    Objective finalObjective = InitiateFinalObjective();
                   
                    if (finalObjective != null)
                    {
                        objective = finalObjective;

                    }
                }
                
                if(ObjectiveManager.Instance.GetCurrentObjective() == objective.hudText)
                {
                    objective.showObjective = false;
                }

                DialogueManager.instance.StartDialogue(currentDialogue,objective, this);

                playerInRange = false;

                if (interractHint != null)
                    interractHint.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
            if (interractHint != null)
            {
                interractHint.SetActive(true);
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
        }
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

    private bool IsTaskCompleted()
    {
        if (PlayerPrefs.GetInt(requiredFlag,0) == 1)
        {
            return true;
        }
        return false;

    }

    private void InitiatePlayerPrefActivateFunction()
    {
        PlayerPrefsActivate playerPrefsActivate = gameObject.GetComponent<PlayerPrefsActivate>();
        if (playerPrefsActivate != null) 
        {
            playerPrefsActivate.activateFlag();
        }
    }

    private Objective InitiateFinalObjective()
    {
        SetFinalObjective setFinalObjective = gameObject.GetComponent<SetFinalObjective>();
        if (setFinalObjective != null)
        {
            return setFinalObjective.ActivateFinalObjective();
        }
        else
        {
            return null;
        }
    }
}
