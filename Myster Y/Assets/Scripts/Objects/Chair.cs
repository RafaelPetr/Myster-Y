using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chair : Interactable {
    public int direction = 1;
    private Animator animator;

    [System.NonSerialized]public UnityEvent<Chair> OnTurnEvent = new UnityEvent<Chair>();

    private void Start() {
        animator = GetComponent<Animator>();

        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(.5f, .5f, 0);
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

        animator.SetFloat("Direction", direction);
    }

    public override void Interact() {
        base.Interact();

        direction++;
        if (direction > 4) {
            direction = 1;
        }

        animator.SetFloat("Direction", direction);
        OnTurnEvent.Invoke(this);
        FinishInteraction();
    }

}
