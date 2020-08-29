using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueSource : MonoBehaviour {
    [System.NonSerialized]public PlayerInventory inventory;
    [System.NonSerialized]public bool inDialogue;
    public void Awake()
    {
        inventory = GameObject.Find("Detective").GetComponent<PlayerInventory>();
    }

    public Dialogue StartDialogue() {
        inDialogue = true;
        return DefineDialogue();
    }

    public void EndDialogue() {
        inDialogue = false;
    }

    public abstract Dialogue DefineDialogue();

}
