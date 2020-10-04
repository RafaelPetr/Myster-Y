﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueSource : MonoBehaviour {
    [System.NonSerialized]public bool inDialogue;

    public void SetInDialogue(bool dialogueStatus) {
        inDialogue = dialogueStatus;
    }

    public Dialogue BuildDialogue() {
        Dialogue dialogue = DefineDialogue();

        dialogue.GetLocalizedText();

        return dialogue;
    }

    public abstract Dialogue DefineDialogue();

}
