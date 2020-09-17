using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueSentence : DialogueElement {

    [TextArea(3,10)]public string text;

    public override void Execute() {
        DialogueManager.instance.localizationIndex++;

        DialogueManager.instance.InitDialogueBox(this);

        string localizedText = LocalizationManager.instance.GetLocalizedValue(DialogueManager.instance.localizationKey,DialogueManager.instance.localizationIndex);
        DialogueManager.instance.StartWriting(localizedText);
    }

    public override void Complete() {
        string text = LocalizationManager.instance.GetLocalizedValue(DialogueManager.instance.localizationKey,DialogueManager.instance.localizationIndex);
        DialogueManager.instance.FinishWrite(text);
    }

}
