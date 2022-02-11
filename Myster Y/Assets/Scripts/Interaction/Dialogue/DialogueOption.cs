using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption {
    public string text;
    public Dialogue linkedDialogue;

    private int localizationGroupIndex;

    public void SetLocalizationGroupIndex(int index) {
        localizationGroupIndex = index;
    }

    public string LocalizeText(string key) {
        return LocalizationManager.instance.GetLocalizedValue(key,localizationGroupIndex);
    }
}
