using System;
using UnityEngine;
    public enum SoundType
    {
        ATTACK,
        FOOTSTEPS,
        //ENEMIE_ATTACK,
        //ENEMIE_SOUND
    }
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private const string SfxVolumeKey = "sfxVolume";//
    public static float SfxVolume = 1f;//
    private void Awake()
    {
        instance = this;

        SfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);//
        DontDestroyOnLoad(gameObject);//
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        float finalVolume = volume * SfxVolume;//

        instance.audioSource.PlayOneShot(randomClip, finalVolume);
    }

    public static void SaveSfxVolume(float value)//
    {
        SfxVolume = value;
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
        PlayerPrefs.Save();
    }//

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}