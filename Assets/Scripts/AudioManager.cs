using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance;
    [SerializeField] private AudioEntry[] audioEntries;
    private Dictionary<AudioType, AudioClip> audioMap;
    [SerializeField]private AudioSource musicSource;
    [SerializeField]private AudioSource SFXSource;

    
    [SerializeField] AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = GetComponent<AudioSource>();
        audioMap = new Dictionary<AudioType, AudioClip>();

        foreach (var entry in audioEntries)
        {
            if (!audioMap.ContainsKey(entry.audioType))
                audioMap.Add(entry.audioType, entry.audioClip);
        }
    }

    public static void PlayAudio(AudioType type, float volume = 1)
    {
        if (Instance.audioMap.TryGetValue(type, out var audioClip))
        {
            Instance.SFXSource.PlayOneShot(audioClip, volume);
        }
    }

    public static void PlayDialogSFX()
    {
        if (Instance.audioMap.TryGetValue(AudioType.NPC_VOICE, out var audioClip))
        {
            Instance.SFXSource.loop = true;
            Instance.SFXSource.clip = audioClip;
            Instance.SFXSource.Play();
        }
    }

    public static void StopDialogSFX()
    {
        Instance.SFXSource.loop = false;
    }

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
