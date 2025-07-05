using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonOutlineHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (outline != null) outline.enabled = true;
        AudioManager.PlayAudio(AudioType.MENU_CHANGE);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null) outline.enabled = false;
    }

}