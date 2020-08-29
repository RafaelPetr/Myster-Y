using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogueElement {
    
    [System.NonSerialized]public DialogueManager dialogueManager;
    public Sprite icon;

    public void defineManager() {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    public abstract void ExecuteElement();
    public abstract void CompleteWrite();
}