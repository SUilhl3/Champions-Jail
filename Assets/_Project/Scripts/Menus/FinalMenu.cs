using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenu : MonoBehaviour
{
    public void onReplay()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Restarting game...");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Entered main menu screen!");
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
