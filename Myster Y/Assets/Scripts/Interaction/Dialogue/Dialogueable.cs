using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialogueable : Interactable {
    public List<Dialogue> dialogues;
    private Item presentedItem;

    public override void Awake() {
        base.Awake();
        foreach (Dialogue dialogue in dialogues) {
            LocalizationManager.instance.ChangeLocalization.AddListener(dialogue.LocalizeElements);
        }
    }

    public override void Interact() {
        DialogueManager.instance.ReceiveInteract(DefineDialogue());
    }

    public abstract Dialogue DefineDialogue();
}
