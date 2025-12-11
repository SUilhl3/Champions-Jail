using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float delayBeforeLoad = 1.5f;

    public void SetDifficultyEasy()
    {
        GameManager.instance.currentDifficulty = GameManager.Difficulty.Easy;
        Debug.Log("Setting to easy...");
    }

    public void SetDifficultyMedium()
    {
        GameManager.instance.currentDifficulty = GameManager.Difficulty.Medium;
        Debug.Log("Setting to medium...");
    }

    public void SetDifficultyHard()
    {
        GameManager.instance.currentDifficulty = GameManager.Difficulty.Hard;
        Debug.Log("Setting to hard...");
    }
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
