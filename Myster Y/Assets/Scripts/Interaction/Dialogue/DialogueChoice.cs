using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice : DialogueElement {
    public bool enabled;
    [TextArea(3,10)]public string context;
    public DialogueOption[] options = new DialogueOption[3];
    
    public override void Execute() {
        foreach (DialogueOption option in options) {
            Debug.Log(option.text);
        }
    }
}
