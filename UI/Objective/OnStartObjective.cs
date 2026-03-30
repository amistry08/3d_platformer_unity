using System.Collections;
using UnityEngine;

public class OnStartObjective : MonoBehaviour
{
    public float delayedStart = 3;
    public Objective objective;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayedStart());
        
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(delayedStart);
        ObjectiveManager.Instance.SetObjective(objective);

        yield return new WaitForSeconds(3f);
        ObjectiveManager.Instance.hudPanel.SetActive(true);
    }
}
