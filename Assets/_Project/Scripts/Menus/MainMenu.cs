using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Game has started...");
    }


    public void ExitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
