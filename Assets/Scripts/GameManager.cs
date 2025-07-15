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
    [Header("Scroll")]
    public Scroll scroll;

    [Header("Class items")]
    [SerializeField] private NPCDataSet npcDataSet;
    [SerializeField] private int kindness = 0;
    [SerializeField] private int faith = 0;
    [SerializeField] private int hope = 0;

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
        TextAsset jsonText = Resources.Load<TextAsset>("Data/npc_data");
        this.npcDataSet = JsonUtility.FromJson<NPCDataSet>(jsonText.text);

        this.SpawnNextNPC();
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
    
    public void ShowOptionButtons(int NPCId)
    {
        this.scroll.NPCId = NPCId;
        this.scroll.ShowOptions();
    }

    public void SpawnNextNPC()
    {
        if (npc.gameObject.activeInHierarchy)
        {
            npc.UnpawnNPC();
        }
        else
        {
            int npcsSize = this.npcDataSet.npcs.Count;
            if (npcsSize > 0)
            {
                int index = Random.Range(0, npcsSize);
                npc.npcData = this.npcDataSet.npcs[index];
                this.npcDataSet.npcs.RemoveAt(index);
                npc.SpawnNPC();
            }
        }
    }
}
