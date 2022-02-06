using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance;

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Throw Dialogue to Manager");
        Debug.Log(dialogue);
    }

}
