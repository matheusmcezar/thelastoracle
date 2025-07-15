using UnityEngine;
using UnityEngine.Localization;

public class Scroll : MonoBehaviour
{
    [SerializeField] private OptionButton option1;
    [SerializeField] private OptionButton option2;
    [SerializeField] private OptionButton option3;
    public int NPCId;

    public async void ShowOptions()
    {
        var localized = new LocalizedString("Languages", "npc-" + this.NPCId + "-option-1");
        this.option1.setOptionText(await localized.GetLocalizedStringAsync().Task);
        localized = new LocalizedString("Languages", "npc-" + this.NPCId + "-option-2");
        this.option2.setOptionText(await localized.GetLocalizedStringAsync().Task);
        localized = new LocalizedString("Languages", "npc-" + this.NPCId + "-option-3");
        this.option3.setOptionText(await localized.GetLocalizedStringAsync().Task);
    }
}
