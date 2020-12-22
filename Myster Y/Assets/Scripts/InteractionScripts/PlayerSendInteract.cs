using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSendInteract : MonoBehaviour {
    public GameObject interactable;
    private void Update() {
        if (Input.GetButtonDown("Interact") && interactable != null) {
            interactable.SendMessage("ExecuteInteract");
        }
    }
}
