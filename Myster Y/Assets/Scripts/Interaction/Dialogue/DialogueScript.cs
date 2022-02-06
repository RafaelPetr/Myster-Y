using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueScript {
    public List<Dialogue> dialogues;
    public abstract Dialogue DefineDialogue();
}
