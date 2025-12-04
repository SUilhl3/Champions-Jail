using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenu : MonoBehaviour
{
    private float delayBeforeLoad = 1.5f;

    public void onReplay()
    {
        Debug.Log("Playing Sound... \nRestarting game...");
        StartCoroutine(LoadLevelWithDelay("Level 1"));
    }

    public void GoToMainMenu()
    {
        Debug.Log("Playing Sound... \n Entering main menu screen");
        StartCoroutine(LoadLevelWithDelay("Main Menu"));
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private IEnumerator LoadLevelWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneName);

    }
}
