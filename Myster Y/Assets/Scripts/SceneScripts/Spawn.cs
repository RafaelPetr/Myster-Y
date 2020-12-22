using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    private Transform playerPos;
    private Transform movePoint;
    private Animator animator;
    [SerializeField]private int idleDirection = 0;

    void Awake() {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        movePoint = GameObject.FindGameObjectWithTag("movePoint").GetComponent<Transform>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    void Start() {
        if (name == SceneController.spawner) {
            playerPos.position = transform.position;
            movePoint.position = transform.position;
            animator.SetFloat("IdleDir",idleDirection);
        }
    }

}
