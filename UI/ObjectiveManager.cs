using System.Collections;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    public static ObjectiveManager Instance;

    [Header("Objective HUD")]
    public GameObject hudPanel;
    public TextMeshProUGUI hudText;

    [Header("Popup")]
    public GameObject objectivePopup;
    public TextMeshProUGUI objectivePopupText;
    public float popupDuration = 4f;

    private string currentObjectiveHud;
    private string currentObjectivePopup;
    private Coroutine popupCoroutine;


    private void Awake()
    {
        if(Instance == null ) Instance= this;
        else Destroy(Instance);
    }

    void Start()
    {
        if(objectivePopup != null) objectivePopup.SetActive(false);
        if(hudPanel != null) hudPanel.SetActive(false);
    }

    public void SetObjective(Objective objective)
    {
        if (objective.showObjective == true)
        {
            currentObjectiveHud = objective.hudText;
            currentObjectivePopup = objective.objectivePopupText;

            if (hudText != null)
            {
                hudText.text = objective.hudText;
            }
            if (objectivePopupText != null)
            {
                objectivePopupText.text = objective.objectivePopupText;
            }

            ShowPopup(currentObjectivePopup);
        }    
    }

    private void ShowPopup(string objective)
    {
        if(popupCoroutine != null) StopCoroutine(popupCoroutine);
        popupCoroutine = StartCoroutine(ShowPopUpRoutine(objective));
    }

    private IEnumerator ShowPopUpRoutine(string objective)
    {
        if(objectivePopupText != null)
        {
            objectivePopupText.text = objective;
            objectivePopup.SetActive(true);
            hudPanel.SetActive(false);

            yield return new WaitForSeconds(popupDuration);

            objectivePopup.SetActive(false);
            hudPanel.SetActive(true);
            popupCoroutine = null;
        }
    }

    public string GetCurrentObjective()
    {
        return currentObjectiveHud;
    }

}
