using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class InteractObject : MonoBehaviour {
    private Dialogue dialogue;
    public GetDialogue getDialogueScript;
    private DialogueManager dialogueManager;

    void Awake() {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    void ExecuteInteract() {
        dialogue = getDialogueScript.DefineDialogue();
        dialogueManager.ReceiveInteract(dialogue);
    }
}
