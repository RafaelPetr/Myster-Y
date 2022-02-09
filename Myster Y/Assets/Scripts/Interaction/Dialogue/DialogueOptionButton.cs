using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueOptionButton : KeyboardButtonUI {
    public int optionIndex;

    public void SendOption() {
        DialogueManager.instance.ReceiveInteract(null,optionIndex);
    }
}