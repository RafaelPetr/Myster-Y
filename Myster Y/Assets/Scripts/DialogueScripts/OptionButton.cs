using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour, ISelectHandler {
    private Button button;
    private EventSystem eventSystem;
    public int choiceNumber;

    void Awake() {
        button = GetComponent<Button>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public void OnSelect(BaseEventData eventData) {
        DialogueManager.instance.selectedOption = button;
    }

    public void SendOption() {
        DialogueManager.instance.PickOption(choiceNumber);
    }
}