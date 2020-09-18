﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour, ISelectHandler {
    private Button button;
    private DialogueManager dialogueManager;
    private EventSystem eventSystem;
    public int choiceNumber;

    void Awake() {
        button = GetComponent<Button>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    public void OnSelect(BaseEventData eventData) {
        //dialogueManager.selectedChoice = button;
    }

    public void SendChoice() {
        if (dialogueManager.inChoice) {
            FindObjectOfType<DialogueManager>().PickOption(choiceNumber);
        }
    }
}