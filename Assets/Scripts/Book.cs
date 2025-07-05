using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour
{
    [Header("Slide")]
    [SerializeField] private Vector2 offScreenPosition = new Vector2(230, 970);
    [SerializeField] private Vector2 onScreenPosition = new Vector2(230, 50);
    [SerializeField] private float slideSpeed = 10f;
    private RectTransform rectTransform;
    private bool isSlinding = false;
    [Header("Pages")]
    [SerializeField] private int pageCount = 4;
    private int currentPage = 1;
    private BookPage[] pages;

    private void Awake()
    {
        if (this.gameObject != null)
        {
            rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = offScreenPosition;
        }
    }

    public void Start()
    {
        pages = this.gameObject.GetComponentsInChildren<BookPage>(true);
        UpdatePage();
    }

    public void NextPage()
    {
        if (currentPage < pageCount)
        {
            currentPage++;
            UpdatePage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        foreach (BookPage page in pages)
        {
            if (page.page == currentPage) page.gameObject.SetActive(true);
            else page.gameObject.SetActive(false);
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
