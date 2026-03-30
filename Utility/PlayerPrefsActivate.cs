using UnityEngine;

public class PlayerPrefsActivate : MonoBehaviour
{
    [Header("Key for PlayerPref")]
    public string[] keyName;

    public void activateFlag()
    {
        if (keyName!= null)
        {
            foreach (var key in keyName)
            {
                PlayerPrefs.SetInt(key, 1);
            }
        }
        
    }
}
