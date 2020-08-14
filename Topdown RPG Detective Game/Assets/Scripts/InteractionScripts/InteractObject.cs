using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class InteractObject : MonoBehaviour {
    private Dialogue dialogue;
    public DialogueSource dialogueSource;
    private DialogueManager dialogueManager;

    void Awake() {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    void ExecuteInteract() {
        dialogueManager.ReceiveInteract(dialogueSource);
    }
}
