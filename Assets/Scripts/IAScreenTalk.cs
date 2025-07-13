using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class IAScreenTalk : MonoBehaviour
{
    #region ATRIBUTES
    [Header("Children")]
    [SerializeField] private TextMeshProUGUI textTMP;
    [Header("Parameters")]
    [SerializeField] private float textSpeed = 0.05f;
    private LocalizedString localizedString;
    private string[] textLines;
    private int currentLine;
    public int NPCId;
    #endregion

    #region TEXT METHODS
    public async void StartTalk()
    {
        this.textTMP.text = string.Empty;
        this.textLines = await this.GetTextLines();
        this.currentLine = 0;
        this.PlayIADialogSFX();
        StartCoroutine(PrintLine());
    }

    IEnumerator PrintLine()
    {
        foreach (char c in textLines[currentLine].ToCharArray())
        {
            textTMP.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void FinishLine()
    {
        StopAllCoroutines();
        textTMP.text = textLines[currentLine];
    }

    private void NextLine()
    {
        if (currentLine < textLines.Length - 1)
        {
            currentLine++;
            textTMP.text = string.Empty;
            this.PlayIADialogSFX();
            StartCoroutine(PrintLine());
        }
        else
        {
            // TODO
        }
    }

    private async Task<string[]> GetTextLines()
    {
        var localized = new LocalizedString
        {
            TableReference = "Languages",
            TableEntryReference = "npc-" + NPCId + "-ia-text"
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

    private void PlayIADialogSFX()
    {
        if (this.currentLine == textLines.Length)
        {
            AudioManager.PlayAudio(AudioType.IA_VOICE_3);
        }
        else if (this.currentLine == 1)
        {
            AudioManager.PlayAudio(AudioType.IA_VOICE_1);
        }
        else
        {
            AudioManager.PlayAudio(AudioType.IA_VOICE_2);
        }
    }
}
