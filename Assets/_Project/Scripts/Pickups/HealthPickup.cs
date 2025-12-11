using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerController pc;
    public float healthIncrease = 20;
    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        pc.AddHealth(healthIncrease);
        pc.UpdateHealthbar();
        Destroy(gameObject);
    }
}
