using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSentence : DialogueElement {
    public DialogueCharacter character;
    [TextArea(3,10)]public string text;

    private int localizationGroupIndex;
    
    public override void Execute() {
        DialogueManager.instance.UpdateSentenceUI(this);
    }

    public void SetLocalizationGroupIndex(int index) {
        localizationGroupIndex = index;
    }

    public void LocalizeText(string key) {
		text = LocalizationManager.instance.GetLocalizedValue(key,localizationGroupIndex);
    }
}
