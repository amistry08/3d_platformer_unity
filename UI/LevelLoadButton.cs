using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This class is meant to be used on buttons as a quick easy way to load levels (scenes)
/// </summary>
public class LevelLoadButton : MonoBehaviour
{
    /// <summary>
    /// Description:
    /// Loads a level according to the name provided
    /// Input:
    /// string levelToLoadName
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="levelToLoadName">The name of the level to load</param>
    public void LoadLevelByName(string levelToLoadName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelToLoadName);
    }

    public void LoadLevel1()
    {
        TransitionLoader.LoadTransition(
            "3:04 AM",
            "Earth outpost - Orion",
            "Talk to Captain Elena Voss",
            "Level1"
        );
    }

    public void LoadLevel2()
    {
        TransitionLoader.LoadTransition(
            "1:45 PM",
            "Nyxara - Base",
            "Investigate.",
            "Level2"
        );
    }

    public void LoadLevel3()
    {
        TransitionLoader.LoadTransition(
            "11:11 PM",
            "Kharon - Unknown",
            "Eliminate the Treat",
            "Level3"
        );
    }
}
