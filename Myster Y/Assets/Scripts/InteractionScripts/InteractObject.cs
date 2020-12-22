using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour {
    public DialogueSource dialogueSource;

    void ExecuteInteract() {
        DialogueManager.instance.ReceiveInteract(dialogueSource);
    }
}
