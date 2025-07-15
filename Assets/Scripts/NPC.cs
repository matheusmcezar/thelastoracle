using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;
using UnityEngine.Rendering.Universal;

public class NPC : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private Vector3 offScreenPosition = new Vector3(12, -0.2f, 1);
    [SerializeField] private Vector3 onScreenPosition = new Vector3(7, -0.2f, 1);
    [SerializeField] private float slideDuration = 0.2f;
    public NPCData npcData;
    private Transform transform;
    private SpriteRenderer spriteRenderer;
    private Sprite NPCSprite;
    private Light2D light;

    private void Awake()
    {
        if (this.gameObject != null)
        {
            this.transform = this.gameObject.GetComponent<Transform>();
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            this.light = this.GetComponent<Light2D>();
        }
    }

    public void SwitchNPCStatus(){
        if (this.gameObject.activeInHierarchy) {
            this.UnpawnNPC();
        } else {
            this.SpawnNPC();
        }
    }

    public void SpawnNPC()
    {
        if (this.gameObject != null && !this.gameObject.activeInHierarchy && npcData.NPCId != 0)
        {
            try
            {
                this.gameObject.SetActive(true);
                NPCSprite = Resources.Load<Sprite>("Sprites/NPCs/npc-" + npcData.NPCId);
                this.spriteRenderer.sprite = NPCSprite;
                this.light.lightCookieSprite = NPCSprite;
                StartCoroutine(Slide(offScreenPosition, onScreenPosition));
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
        }
    }

    public void UnpawnNPC()
    {
        if (this.gameObject != null && this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(Slide(onScreenPosition, offScreenPosition, false));
        }
    }

    private IEnumerator Slide(Vector3 start, Vector3 end, bool slidingIn = true)
    {
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / slideDuration);
            transform.localPosition = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.localPosition = end;

        if (slidingIn)
        {
            GameManager.Instance.StartTalk(this.npcData.NPCId);
        }
        else
        {
            GameManager.Instance.FinishTalk();
            this.gameObject.SetActive(false);
        }
    }
}
