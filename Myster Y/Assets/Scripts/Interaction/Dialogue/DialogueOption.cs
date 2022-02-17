using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption {
    public string text;
    public Dialogue linkedDialogue;

    public int localizationGroupIndex;

    public void LocalizeText(string key) {
        text = LocalizationManager.instance.GetLocalizedValue(key,localizationGroupIndex);
    }
}
