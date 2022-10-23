using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialogueable : Interactable {
    [System.NonSerialized]public string key;
    [System.NonSerialized]public string initialKey;
    [System.NonSerialized]public bool freezeDialogue;
    
    public List<Dialogue> dialogues;
    public Dictionary<string, Dialogue> dialogueDict = new Dictionary<string, Dialogue>();

    [System.NonSerialized]public Dialogue activeDialogue;
    [System.NonSerialized]public List<string> backupTexts = new List<string>();

    public override void Awake() {
        base.Awake();

        foreach (Dialogue dialogue in dialogues) {
            dialogueDict[dialogue.GetKey()] = Instantiate(dialogue);
        }

        ResetKey();
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

    public override void FinishInteraction() {
        base.FinishInteraction();

        ResetOptions();
    }

    public virtual void ResetKey() {
        initialKey = "scriptable_dialogue_";
        key = string.Copy(initialKey);
    }

    public virtual Dialogue DefineDialogue() {
        key = dialogues[0].GetKey();
        return Instantiate(dialogues[0]);
    }

    public virtual void ExecuteFunction(string function) {}

    public void SaveOptions() {
        DialogueChoice choice = dialogueDict[key].GetChoice();

        if (choice != null) {
            foreach (DialogueOption option in choice.GetOptions()) {
                backupTexts.Add(string.Copy(option.GetText()));
            }
        }
    }

    public void ResetOptions() {
        DialogueChoice choice = dialogueDict[key].GetChoice();

        if (backupTexts.Count > 0) {
            for (int i = 0; i < backupTexts.Count; i++) {
                choice.GetOption(i).SetText(string.Copy(backupTexts[i]));
            }
            backupTexts.Clear();
        }
    }

    public void RemoveOption(int index) {
        DialogueOption option = dialogueDict[key].GetChoice().GetOption(index);
        option.SetText("");
    }

    public void StartDialogue(Dialogue dialogue) {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
