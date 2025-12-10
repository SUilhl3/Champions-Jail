using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    //musidc settings
    [SerializeField] Slider musicSlider;
    public AudioSource audioSource;

    //SFX settings
    public Slider sfxSlider;

    private const string VolumeKey = "musicVolume";
    private const string SfxVolumeKey = "sfxVolume";

    void Start()
    {
        InitializeMusicVolume();
        InitializeSfxVolume();
    }

    #region Music Volume
    private void InitializeMusicVolume()
    {
        if (!PlayerPrefs.HasKey(VolumeKey))
        {
            PlayerPrefs.SetFloat(VolumeKey, 1f);
            PlayerPrefs.Save();
        }

        LoadMusicVolume();

        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        }
    }

    public void ChangeMusicVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }
        SaveMusicVolume(value);
    }

    private void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    private void LoadMusicVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey);

        if (musicSlider != null)
        {
            musicSlider.value = savedVolume;
        }

        if (audioSource != null)
        {
            audioSource.volume = savedVolume;
        }
    }
    #endregion

    #region SFX Volume
    private void InitializeSfxVolume()
    {
        if (sfxSlider == null) return;

        float savedVolume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);
        sfxSlider.value = savedVolume;

        SoundManager.SaveSfxVolume(savedVolume);

        sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    private void ChangeSfxVolume(float value)
    {
        SoundManager.SaveSfxVolume(value);
    }
    #endregion
}
