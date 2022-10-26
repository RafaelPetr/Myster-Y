using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chair : Interactable {
    private int direction = 1;
    private Animator animator;

    [System.NonSerialized]public UnityEvent<Chair> OnTurnEvent = new UnityEvent<Chair>();

    public override void Start() {
        base.Start();
        animator = GetComponent<Animator>();
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

    #region Getters

        public int GetDirection() {
            return direction;
        }
    
    #endregion

    #region Setters

        public void SetDirection(int value) {
            direction = value;
        }

    #endregion
}
