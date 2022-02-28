using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPointer : MonoBehaviour {
    public LayerMask interactableLayer;

    private Vector3 interactablePosition;
    private bool enableInteract;
    private bool freezePosition;
    private Interactable interactable;

    private void Start() {
        transform.parent = null;
    }

    void Update() {
        if (freezePosition) {
            transform.position = interactablePosition;
        }
        if (enableInteract && Input.GetButtonDown("Interact")) {
            freezePosition = true;
            interactable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Interactable")) {
            interactable = collider.GetComponent<Interactable>();
            interactablePosition = transform.position;
            enableInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.CompareTag("Interactable")) {
            interactable = null;
            enableInteract = false;
        }
    }

    public void SetFreezePosition(bool value) {
        freezePosition = value;
    }

    public void Move(Vector3 position) {
        transform.position = position;
    }
}