using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInteractable : MonoBehaviour {
    public PlayerSendInteract playerInteract;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Interactable")) {
            playerInteract.interactable = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Interactable")) {
            if (playerInteract.interactable == other.gameObject) {
                playerInteract.interactable = null;
            }
        }
    }
}
