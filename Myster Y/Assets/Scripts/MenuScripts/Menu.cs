using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour, ISelectHandler, IDeselectHandler {
    private Button button;
    public bool selected;

    private void Awake() {
        button = GetComponent<Button>();
    }

    private void Start() {
        if (selected) {
            button.Select();
        }
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
