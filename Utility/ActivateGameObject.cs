using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    [Header("PlayerPref key name")]
    public string key;

    void Awake()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

        }    
    }

}
