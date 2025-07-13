using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Book")]
    public Book book;

    [Header("NPC")]
    public NPC npc;

    [Header("Baloon Talk")]
    public BaloonTalk baloonTalk;
    
    [Header("IA Screen Talk")]
    public IAScreenTalk iaScreenTalk;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //npc.SpawnNPC();
    }

    public void HandleBook()
    {
        if (book != null)
        {
            book.Handle();
        }
    }

    public void StartTalk(int NPCId)
    {
        this.baloonTalk.NPCId = NPCId;
        this.baloonTalk.gameObject.SetActive(true);
    }

    public void FinishTalk()
    {
        this.baloonTalk.gameObject.SetActive(false);
    }

    public void StartIATalk(int NPCId)
    {
        this.iaScreenTalk.NPCId = NPCId;
        this.iaScreenTalk.StartTalk();
    }

}
