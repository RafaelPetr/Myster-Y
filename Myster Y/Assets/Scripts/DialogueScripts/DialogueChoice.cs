using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice : DialogueElement {

    public List<string> options = new List<string>();

    public void AddOption() {
        if (options.Count < 3) {
            options.Add(string.Empty);
        }
    }

    public void RemoveOption(int index) {
        if (options.Count > 0) {
            options.RemoveAt(index);
        }
    }

    public override void Execute() {
        DialogueManager.instance.localizationIndex++;

        DialogueManager.instance.InitDialogueBox(this);

        string localizedText = LocalizationManager.instance.GetLocalizedValue(DialogueManager.instance.localizationKey,DialogueManager.instance.localizationIndex);
        DialogueManager.instance.StartWriting(localizedText);

        List<string> localizedOptions = new List<string>();
        for (int i = 0; i < options.Count; i++) {
            localizedText = LocalizationManager.instance.GetLocalizedValue(DialogueManager.instance.localizationKey,DialogueManager.instance.localizationIndex);
            localizedOptions.Add(localizedText);
            DialogueManager.instance.localizationIndex++;
        }

        DialogueManager.instance.DefineChoiceButtons(localizedOptions);
    }

    public override void Complete() {
        Debug.Log("Execute choice");
    }
    
}
