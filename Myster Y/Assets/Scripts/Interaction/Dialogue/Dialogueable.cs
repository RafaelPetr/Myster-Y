using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialogueable : Interactable {
    public List<Dialogue> dialogues;

    public override void Interact() {
        if (!DialogueManager.instance.GetIsChoosing()) {
            DialogueManager.instance.ReceiveInteract(DefineDialogue());
        }
    }

    public abstract Dialogue DefineDialogue();
}
