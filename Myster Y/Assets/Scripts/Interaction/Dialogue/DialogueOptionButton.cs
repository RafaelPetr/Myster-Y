using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueOptionButton : KeyboardButtonUI {
    public int index;

    public override void OnSelect(BaseEventData eventData) {
        DialogueManager.instance.SetOptionIndex(index);
    }
}