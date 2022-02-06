using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogueable : Interactable {
    public DialogueScript dialogueScript;

    public override void Interact() {
        DialogueManager.instance.StartDialogue(dialogueScript.DefineDialogue());
    }
}
