using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private PlayerController controller;
    private Animator animator;
    private AnimatorStateInfo currentAnimation;
    private float animationTime;

    //Inventory
    private float currentAnimationOpenInventory;

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
            if (!controller.GetExitInventoryTrigger()) {
                if (controller.GetInventory() != currentAnimationOpenInventory) { //If controller's inventory panel is different from current animation open inventory
                    if (animationTime <= 0.01f) {
                        currentAnimationOpenInventory = controller.GetInventory();
                    }
                }
                else {
                    if (controller.GetOpenInventoryTrigger()) {
                        if (controller.GetEnterInventoryTrigger()) {
                            animationTime = 0f;
                            controller.SetEnterInventoryTrigger(false);
                        }
                        animator.SetFloat("Inventory",currentAnimationOpenInventory);
                        animator.Play("Open Inventory", 0, animationTime);
                        controller.SetOpenInventoryTrigger(false);
                    }

                    if (controller.GetCloseInventoryTrigger()) {
                        animator.Play("Close Inventory", 0, animationTime);
                        controller.SetCloseInventoryTrigger(false);
                    }
                }
            }
            else if (animationTime <= 0.01f) {
                animator.SetTrigger("Exit Inventory");
                controller.SetExitInventoryTrigger(false);
            }
        }
    }

}