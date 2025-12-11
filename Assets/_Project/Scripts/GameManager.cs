using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum Difficulty { Easy, Medium, Hard}
    public Difficulty currentDifficulty;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); ;
        }else Destroy(gameObject);
    }
}
