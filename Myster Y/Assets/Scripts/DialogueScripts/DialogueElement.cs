using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class DialogueElement {

    public DialogueCharacter character;

    public abstract void Execute();

}