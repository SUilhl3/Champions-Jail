using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float delayBeforeLoad = 1.5f;
    public void OnNewGameClicked()
    {
        Debug.Log("Playing Sound...");
        StartCoroutine(LoadLevelWithDelay("Level 1"));
    }


    public void ExitGame()
    {
            Debug.Log("Quitting Game...");
            Application.Quit();
    }
    private IEnumerator LoadLevelWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("Level 1");
        Debug.Log("Game has started...");

    }
}

