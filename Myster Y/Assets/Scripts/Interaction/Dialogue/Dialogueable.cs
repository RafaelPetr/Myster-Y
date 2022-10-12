using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialogueable : Interactable {
    [System.NonSerialized]public string key;
    
    public List<Dialogue> dialogues;
    public Dictionary<string, Dialogue> dialogueDict = new Dictionary<string, Dialogue>();

    public override void Awake() {
        base.Awake();

        key = "scriptable_dialogue_";

        foreach (Dialogue dialogue in dialogues) {
            dialogueDict[dialogue.key] = dialogue;
        }
    }

    public override void Start() {
        base.Start();
        foreach (Dialogue dialogue in dialogues) {
            LocalizationManager.instance.ChangeLocalization.AddListener(dialogue.LocalizeElements);
        }
    }

    public override void Interact() {
        base.Interact();
        DialogueManager.instance.ReceiveInteract(this);
    }

    public virtual Dialogue DefineDialogue() {
        return dialogues[0];
    }

    public virtual void ExecuteFunction(string function) {}
}
