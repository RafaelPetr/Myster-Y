using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice : DialogueElement {
    [SerializeField]private bool enable;
    [TextArea(3,10)]public string context;
    public List<DialogueOption> options = new List<DialogueOption>();

    public int contextLocalizationGroupIndex;

    public bool GetEnable() {
        return enable;
    }

    public void SetEnable(bool value) {
        enable = value;
    }

    public override void Execute() {
        DialogueManager.instance.UpdateChoiceUI(this);
    }

    public void LocalizeText(string key) {
		context = LocalizationManager.instance.GetLocalizedValue(key,contextLocalizationGroupIndex);

        foreach (DialogueOption option in options) {
            option.LocalizeText(key);
        }
    }

    public void SetContextLocalizationGroupIndex(int index) {
        contextLocalizationGroupIndex = index;
    }
}
