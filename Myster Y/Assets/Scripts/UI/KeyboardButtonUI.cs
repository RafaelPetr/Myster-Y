using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardButtonUI : MonoBehaviour, ISelectHandler, IDeselectHandler {
    private Button button;
    private bool selected;

    private void Awake() {
        button = GetComponent<Button>();
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && selected) {
            button.onClick.Invoke();
        }
    }

    public void OnSelect(BaseEventData eventData) {
        selected = true;
    }

    public void OnDeselect(BaseEventData eventData) {
        selected = false;
    }
}