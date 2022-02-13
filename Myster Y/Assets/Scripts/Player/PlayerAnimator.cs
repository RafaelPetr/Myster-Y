using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private PlayerController controller;
    private Animator animator;
    private AnimatorStateInfo currentAnimation;
    private float animationTime;

    //Inventory
    private float toOpenInventory;

    void Start() {
        animator = GetComponent<Animator>();
        controller = PlayerController.instance;
    }

    void Update() {
        currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        animationTime = Mathf.Lerp(0, 1, 1 - currentAnimation.normalizedTime);

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
        if (controller.GetInInventory()) {
            if (!controller.GetExitInventory()) {
                if (animationTime <= 0.01f) {
                    toOpenInventory = 0;
                }

                if (controller.GetOpenInventory() && toOpenInventory == 0) {
                    animator.SetFloat("Inventory",controller.GetInventory());
                    animator.Play("Open Inventory", 0, animationTime);
                    controller.SetOpenInventory(false);
                }

                if (controller.GetCloseInventory()) {
                    animator.Play("Close Inventory", 0, animationTime);
                    controller.SetCloseInventory(false);
                }
                
            }
            else if (animationTime <= 0.01f) {
                animator.SetTrigger("Exit Inventory");
                controller.SetExitInventory(false);
            }
        }
    }
}
