using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    private void Awake() {
        gameObject.tag = "Interactable";
    }

    public void Interact() {
        PlayerController.instance.SetInInteraction(false);
        Debug.Log("Interaction");
    }
}
