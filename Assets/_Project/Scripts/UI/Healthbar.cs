using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void UpdateHealthBar(float value)
    {
        slider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
