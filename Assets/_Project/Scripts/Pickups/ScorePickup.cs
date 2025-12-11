using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    PlayerController pc;
    public int scoreIncrease = 20;
    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        pc.AddPoints(scoreIncrease);
        Destroy(gameObject);
    }
}
