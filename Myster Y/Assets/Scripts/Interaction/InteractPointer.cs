using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPointer : MonoBehaviour {
    public LayerMask interactableLayer;

    private Interactable interactable;
    private Interactable activeInteractable;

    private void Start() {
        transform.parent = null;
    }

    void Update() {
        if (Input.GetButtonDown("Interact")) {
            if (activeInteractable != null) {
                activeInteractable.Interact();
            }
            else if (interactable != null) {
                interactable.Interact();
                activeInteractable = interactable;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();
        
        if (tags != null) {
            if (tags.HasTag(Tag.Interactable)) {
                interactable = other.GetComponent<Interactable>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();
        
        if (tags != null) {
            if (tags.HasTag(Tag.Interactable)) {
                interactable = null;
            }
        }
    }

    public void Move(Vector3 position) {
        transform.position = position;
    }

    public void FinishInteraction() {
        activeInteractable = null;
    }
}