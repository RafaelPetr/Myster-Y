using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class InteractObject : MonoBehaviour {
    public DialogueSource dialogueSource;

    void ExecuteInteract() {
        DialogueManager.instance.ReceiveInteract(dialogueSource);
    }
}
