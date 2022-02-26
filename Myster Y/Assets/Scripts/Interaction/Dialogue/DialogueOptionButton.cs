using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueOptionButton : MonoBehaviour, ISelectHandler {
    public int index;

    public void OnSelect(BaseEventData eventData) {
        DialogueManager.instance.SetOptionIndex(index);
    }
}