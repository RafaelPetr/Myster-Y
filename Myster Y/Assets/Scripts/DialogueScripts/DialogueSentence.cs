using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueSentence : DialogueElement {

    [TextArea(3,10)]public string text;

    public override void Execute() {
        DialogueManager.instance.UpdateDialogueBox(this);
		DialogueManager.instance.StartWriting(this.text);
    }

}
