using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class BaloonTalk : MonoBehaviour
{
    #region ATRIBUTES
    [Header("Children")]
    [SerializeField] private RectTransform imageRect;
    [SerializeField] private TextMeshProUGUI textTMP;
    [Header("Parameters")]
    [SerializeField] private float animationDuration = 0.1f;
    [SerializeField] private float textSpeed = 0.05f;
    private float originalHeight;
    private LocalizedString localizedString;
    private string[] textLines;
    private int currentLine;
    public int NPCId;
    #endregion

    #region DEFAULT METHODS
    private void Awake()
    {
        originalHeight = imageRect.sizeDelta.y;
        imageRect.sizeDelta = new Vector2(imageRect.sizeDelta.x, 0);
    }

    private void OnEnable()
    {
        StartCoroutine(RevealCoroutine());
    }
    #endregion

    #region SHOW/HIDE
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
        GameManager.Instance.StartIATalk(NPCId);
    }
    #endregion

    #region TEXT METHODS
    private async void StartTalk()
    {
        this.textTMP.text = string.Empty;
        this.textLines = await this.GetTextLines();
        this.currentLine = 0;
        AudioManager.PlayDialogSFX();
        StartCoroutine(PrintLine());
    }

    IEnumerator PrintLine()
    {
        foreach (char c in textLines[currentLine].ToCharArray()) {
            textTMP.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        AudioManager.StopDialogSFX();
    }

    private void FinishLine()
    {
        StopAllCoroutines();
        textTMP.text = textLines[currentLine];
        AudioManager.StopDialogSFX();
    }

    private void NextLine()
    {
        if (currentLine < textLines.Length - 1) {
            currentLine++;
            textTMP.text = string.Empty;
            AudioManager.PlayDialogSFX();
            StartCoroutine(PrintLine());
        } else {
            AudioManager.StopDialogSFX();
            this.Hide();
        }
    }

    private async Task<string[]> GetTextLines()
    {
        var localized = new LocalizedString
        {
            TableReference = "Languages",
            TableEntryReference = "npc-" + this.NPCId + "-text"
        };

        string text = await localized.GetLocalizedStringAsync().Task;

        return text.Split("\n");
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
    #endregion
}