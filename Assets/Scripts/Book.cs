using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour
{
    [Header("Book")]
    public Vector2 offScreenPosition = new Vector2(230, 970);
    public Vector2 onScreenPosition = new Vector2(230, 50);
    public float slideSpeed = 10f;
    private RectTransform rectTransform;
    private bool isSlinding = false;

    private void Awake()
    {
        if (this.gameObject != null)
        {
            rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = offScreenPosition;
        }
    }

    public void Handle()
    {
        if (this.gameObject != null && !isSlinding)
        {
            if (this.gameObject.activeInHierarchy)
            {
                isSlinding = true;
                AudioManager.PlayAudio(AudioType.BOOK_CLOSE);
                StartCoroutine(SlideOut());
            }
            else
            {
                isSlinding = true;
                AudioManager.PlayAudio(AudioType.BOOK_OPEN);
                this.gameObject.SetActive(true);
                StartCoroutine(SlideIn());
            }
        }
    }

    private IEnumerator SlideIn()
    {
        rectTransform.anchoredPosition = offScreenPosition;

        while (Vector2.Distance(rectTransform.anchoredPosition, onScreenPosition) > 1f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                onScreenPosition,
                Time.deltaTime * slideSpeed
            );
            yield return null;
        }

        rectTransform.anchoredPosition = onScreenPosition;
        isSlinding = false;
    }

    private IEnumerator SlideOut()
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, offScreenPosition) > 10f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                offScreenPosition,
                Time.deltaTime * slideSpeed
            );
            yield return null;
        }

        rectTransform.anchoredPosition = offScreenPosition;
        this.gameObject.SetActive(false);
        isSlinding = false;
    }
}
