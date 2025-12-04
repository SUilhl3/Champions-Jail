using UnityEngine;

public class PlayFootSteps : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.PlaySound(SoundType.FOOTSTEPS);
    }
}
