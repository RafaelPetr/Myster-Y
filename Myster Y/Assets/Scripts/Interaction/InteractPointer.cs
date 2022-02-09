using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPointer : MonoBehaviour {
    public LayerMask interactableLayer;

    private bool enableInteract;
    private Interactable interactable;

    void Update() {
        if (enableInteract && Input.GetButtonDown("Interact")) {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Interactable")) {
            interactable = collider.GetComponent<Interactable>();
            enableInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.CompareTag("Interactable")) {
            interactable = null;
            enableInteract = false;
        }
    }
}
