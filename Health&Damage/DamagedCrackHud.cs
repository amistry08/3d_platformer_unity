using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DamageCrackHUD : MonoBehaviour
{
    public static DamageCrackHUD Instance;

    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Damage Flash")]
    [SerializeField] private float hitAlpha = 0.5f;
    [SerializeField] private float fadeSpeed = 3f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha,
                0f,
                fadeSpeed * Time.deltaTime
            );
        }
    }

    public void ShowDamage()
    {
        canvasGroup.alpha = hitAlpha;
    }

    public void ShowDamage(float alpha)
    {
        canvasGroup.alpha = Mathf.Clamp01(alpha);
    }
}