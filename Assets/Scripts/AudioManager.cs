using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance;
    [SerializeField] private AudioEntry[] audioEntries;
    private Dictionary<AudioType, AudioClip> audioMap;
    private AudioSource audioSource;
    
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

        audioSource = GetComponent<AudioSource>();
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
            Instance.audioSource.PlayOneShot(audioClip, volume);
        }
    }
    void Start()
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
}
