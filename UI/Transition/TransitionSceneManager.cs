using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneManager : MonoBehaviour
{

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI planetText;
    public TextMeshProUGUI objectiveText;

    [Header("Timing")]
    public float firstDelay = 1f;
    public float stepDelay = 1f;
    public float fadeIn = 1f;
    public float fadeOut = 1f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadNextScene()
    {
        if (!string.IsNullOrEmpty(TransitionData.nextSceneName))
        {
            SceneManager.LoadScene(TransitionData.nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next Scene name is Empty");
        }
    }

    IEnumerator PlaySequence()
    {

        timeText.text = TransitionData.timeText;
        planetText.text = TransitionData.planetText;
        objectiveText.text = TransitionData.objectiveText;

        // Start all invisible
        SetAlpha(timeText, 0f);
        SetAlpha(planetText, 0f);
        SetAlpha(objectiveText, 0f);


        yield return new WaitForSeconds(firstDelay);

        // TIME TEXT
        yield return StartCoroutine(FadeText(timeText, 0f, 1f, fadeIn));
        yield return new WaitForSeconds(stepDelay);

        // PLANET TEXT
        yield return StartCoroutine(FadeText(planetText, 0f, 1f, fadeIn));
        yield return new WaitForSeconds(stepDelay);

        // FADE OUT PLANET
        yield return StartCoroutine(FadeText(planetText, 1f, 0f, fadeOut));
        yield return new WaitForSeconds(stepDelay);

        // OBJECTIVE TEXT
        yield return StartCoroutine(FadeText(objectiveText, 0f, 1f, fadeIn));
        yield return new WaitForSeconds(stepDelay);

        // FADE OUT ALL
        yield return StartCoroutine(FadeText(timeText, 1f, 0f, fadeOut));
        yield return StartCoroutine(FadeText(objectiveText, 1f, 0f, fadeOut));

        yield return new WaitForSeconds(stepDelay);
        loadNextScene();
    }

    // Helper: fade text alpha
    IEnumerator FadeText(TextMeshProUGUI text, float start, float end, float duration)
    {
        float time = 0f;

        Color color = text.color;
        color.a = start;
        text.color = color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);

            color.a = Mathf.Lerp(start, end, t);
            text.color = color;

            yield return null;
        }

        color.a = end;
        text.color = color;
    }

    void SetAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}
