using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption {
    public string text;
    public string function;

    public int localizationGroupIndex;

    public void LocalizeText(string key) {
        text = LocalizationManager.instance.GetLocalizedValue(key,localizationGroupIndex);
    }
}
