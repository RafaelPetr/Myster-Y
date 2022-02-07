using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSentence : DialogueElement {
    [TextArea(3,10)]public string text;
    
    public override void Execute() {
        Debug.Log(text);
    }
}
