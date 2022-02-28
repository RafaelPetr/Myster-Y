using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    private bool interacting;

    public virtual void Awake() {
        gameObject.tag = "Interactable";
    }

    public virtual void Interact() {
        PlayerController.instance.SetInInteraction(true);
        interacting = true;
    }

    public void FinishInteraction() {
        interacting = false;
        PlayerController.instance.SetInInteraction(false);
    }
}
