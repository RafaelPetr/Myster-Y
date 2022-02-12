using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private Animator animator;
    private PlayerController controller;

    void Start() {
        animator = GetComponent<Animator>();
        controller = PlayerController.instance;
    }

    void Update() {
        AnimateMovement();
        AnimateInventory();
    }

    private void AnimateMovement() {
        animator.SetFloat("DirectionX",controller.GetDirectionX());
        animator.SetFloat("DirectionY",controller.GetDirectionY());
        animator.SetBool("Walking",controller.GetWalking());
        animator.SetBool("Running",controller.GetRunning());
    }

    private void AnimateInventory() {
        animator.SetFloat("Inventory",controller.GetInventory());

        if (controller.GetOpenInventory()) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Close Inventory")) {
                return;
            }
            controller.SetOpenInventory(false);
            animator.SetTrigger("Open Inventory");
        }

        if (controller.GetCloseInventory()) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open Inventory")) {
                return;
            }
            controller.SetCloseInventory(false);
            animator.SetTrigger("Close Inventory");
            controller.SetInventory(0f);
        }

        //float animationTime = Mathf.Lerp(0, 1, 1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        
    }
}
