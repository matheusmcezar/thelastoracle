using UnityEngine;

[System.Serializable]
public class AudioEntry
{
    public AudioType audioType;
    public AudioClip audioClip;
}

public enum AudioType
{
    BOOK_OPEN, BOOK_CLOSE, MENU_CHANGE, MENU_CLICK, NPC_VOICE, IA_VOICE_1, IA_VOICE_2, IA_VOICE_3
}
