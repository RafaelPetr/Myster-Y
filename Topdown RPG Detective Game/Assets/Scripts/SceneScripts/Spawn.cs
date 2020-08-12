using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    private Transform playerPos;
    private Transform movePoint;
    private Animator animator;
    [SerializeField]private int idleDirection = 0;

    void Awake() {
        playerPos = GameObject.Find("Detective").GetComponent<Transform>();
        movePoint = GameObject.Find("MovePoint").GetComponent<Transform>();
        animator = GameObject.Find("Detective").GetComponent<Animator>();
    }
    void Start() {
        if (name == SceneController.spawner)
        {
            playerPos.position = transform.position;
            movePoint.position = transform.position;
            animator.SetFloat("IdleDir",idleDirection);
        }
    }

}
