using TMPro;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionText;

    public void setOptionText(string text)
    {
        this.optionText.text = text;
    }
}
