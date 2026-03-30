using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLoader
{
    public static void LoadTransition(
        string time,
        string planet,
        string objective,
        string nextScene)
    {
        TransitionData.timeText = time;
        TransitionData.planetText = planet;
        TransitionData.objectiveText = objective;
        TransitionData.nextSceneName = nextScene;

        SceneManager.LoadScene("TransitionScene");
    }
}
