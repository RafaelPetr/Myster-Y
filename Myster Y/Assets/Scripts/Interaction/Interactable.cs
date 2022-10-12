using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    private bool inInteraction;

    public virtual void Awake() {
        CustomTags tags = gameObject.AddComponent<CustomTags>();
        tags.AddTag(Tag.Interactable);
    }

    public virtual void Start() {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(.5f, .5f, 0);
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    }

    public virtual void Interact() {
        TimeManager.instance.SetPauseTime(true);
        PlayerController.instance.SetInInteraction(true);
        inInteraction = true;
    }

    public virtual void FinishInteraction() {
        TimeManager.instance.SetPauseTime(false);
        PlayerController.instance.interactPointer.FinishInteraction();
        PlayerController.instance.SetInInteraction(false);
    }

    public bool GetInInteraction() {
        return inInteraction;
    }
}
