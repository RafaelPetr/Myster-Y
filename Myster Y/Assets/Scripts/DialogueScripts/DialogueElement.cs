using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueElement {
    
    [System.NonSerialized]public DialogueManager dialogueManager;
    public Sprite icon;

    public void defineManager() {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    //public void ExecuteElement();
    //public void CompleteWrite();
}