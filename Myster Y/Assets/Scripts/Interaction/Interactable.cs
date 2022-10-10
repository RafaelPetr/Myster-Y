using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    private bool inInteraction;

    public virtual void Awake() {
        CustomTags tags = gameObject.AddComponent<CustomTags>();
        tags.AddTag(Tag.Interactable);
    }

    public virtual void Interact() {
        TimeManager.instance.SetPauseTime(true);
        PlayerController.instance.SetInInteraction(true);
        inInteraction = true;
    }

    public void FinishInteraction() {
        TimeManager.instance.SetPauseTime(false);
        PlayerController.instance.interactPointer.FinishInteraction();
        PlayerController.instance.SetInInteraction(false);
    }

    public bool GetInInteraction() {
        return inInteraction;
    }
}
