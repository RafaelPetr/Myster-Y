using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour {
    private NPC npc;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        npc = GetComponent<NPC>();
    }

    void Update() {
        AnimateMovement();
    }

    private void AnimateMovement() {
        animator.SetFloat("DirectionX",npc.GetDirectionX());
        animator.SetFloat("DirectionY",npc.GetDirectionY());
        animator.SetBool("Moving",npc.GetMoving());
    }
}
