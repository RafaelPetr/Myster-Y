using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialogueable : Interactable {
    public List<Dialogue> dialogues;

    public override void Interact() {
        if (DialogueManager.instance.GetIsWriting()) {
            DialogueManager.instance.FinishWrite();
            return;
        }
        //If in choice
        else if (PlayerController.instance.GetInInteraction()) {
            DialogueManager.instance.ExecuteNextElement();
        }       
        else {
            DialogueManager.instance.StartDialogue(DefineDialogue());
            PlayerController.instance.SetInInteraction(true);
        }
    }

    public abstract Dialogue DefineDialogue();
}
