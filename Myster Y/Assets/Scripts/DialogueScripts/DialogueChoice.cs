using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice : DialogueElement {

    public List<DialogueOption> options = new List<DialogueOption>();

    public void AddOption() {
        if (options.Count < 3) {
            options.Add(new DialogueOption());
        }
    }

    public void RemoveOption(int index) {
        if (options.Count > 0) {
            options.RemoveAt(index);
        }
    }

    public override void Execute() {
        DialogueManager.instance.inChoice = true;
        DialogueManager.instance.UpdateDialogueBox(this);
        DialogueManager.instance.DefineChoiceButtons(this.options);
    }

}
