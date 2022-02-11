using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public virtual void Awake() {
        gameObject.tag = "Interactable";
    }

    public abstract void Interact();
}
