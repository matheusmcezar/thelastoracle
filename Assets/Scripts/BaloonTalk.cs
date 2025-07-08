using UnityEngine;
using System.Collections;
using TMPro;

public class BaloonTalk : MonoBehaviour
{
    [Header("Children")]
    [SerializeField] private RectTransform imageRect;
    [SerializeField] private TextMeshProUGUI textTMP;
    [Header("Parameters")]
    [SerializeField] private float animationDuration = 0.1f;
    [SerializeField] private float textSpeed = 0.05f;
    private float originalHeight;
    private string[] textLines;
    private int currentLine;

    private void Awake()
    {
        originalHeight = imageRect.sizeDelta.y;
        imageRect.sizeDelta = new Vector2(imageRect.sizeDelta.x, 0);
    }

    private void OnEnable()
    {
        StartCoroutine(RevealCoroutine());
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideCoroutine());
    }

    private IEnumerator RevealCoroutine()
    {
        float time = 0f;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / animationDuration);
            float height = Mathf.Lerp(0, originalHeight, t);
            imageRect.sizeDelta = new Vector2(imageRect.sizeDelta.x, height);
            yield return null;
        }
        this.textTMP.gameObject.SetActive(true);
        this.StartTalk();
    }

    private IEnumerator HideCoroutine()
    {
        this.textTMP.gameObject.SetActive(false);
        float time = 0f;
        float startHeight = imageRect.sizeDelta.y;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / animationDuration);
            float height = Mathf.Lerp(startHeight, 0, t);
            imageRect.sizeDelta = new Vector2(imageRect.sizeDelta.x, height);
            yield return null;
        }
        this.gameObject.SetActive(false);
    }

    // TODO: transformar e sessão de código
    // TEXT METHODS
    private void StartTalk()
    {
        this.textTMP.text = string.Empty;
        this.textLines = this.GetTextLines();
        this.currentLine = 0;
        //AudioManager.PlayDialogSFX(audioManager.dialogWrite);
        StartCoroutine(PrintLine());
    }

    IEnumerator PrintLine()
    {
        foreach (char c in textLines[currentLine].ToCharArray()) {
            textTMP.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        //AudioManager.StopDialogSFX();
    }

    public void HandleClick()
    {
        if (textTMP.text == textLines[currentLine])
        {
            NextLine();
        }
        else
        {
            FinishLine();
        }
    }

    private void FinishLine()
    {
        StopAllCoroutines();
        textTMP.text = textLines[currentLine];
        //AudioManager.StopDialogSFX();
    }

    private void NextLine()
    {
        if (currentLine < textLines.Length - 1) {
            currentLine++;
            textTMP.text = string.Empty;
            //AudioManager.PlayDialogSFX(audioManager.dialogWrite);
            StartCoroutine(PrintLine());
        } else {
            //AudioManager.StopDialogSFX();
            this.Hide();
        }
    }

    private string[] GetTextLines()
    {
        return new string[] {"Oi", "Isso e uma string de teste.", "As quebras de linha são linhas diferentes"};
    }
}
