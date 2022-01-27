using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    private float moveSpeed = 1.5f;
    private bool walking;
    private bool running;

    [System.NonSerialized]public float directionX;
    [System.NonSerialized]public float directionY = -1;

    private bool inDialogue;
    private bool inTransition;

    public LayerMask stopMove;

    public Transform movePoint;
    public Transform interactPointer;

    private Animator animator;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        movePoint.parent = null;
        interactPointer.parent = null;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, Time.deltaTime * moveSpeed);
    }

    void Update() {

        if (Input.GetButton("Run")) {
            running = true;
            moveSpeed = 2.5f;
        }
        if (Input.GetButtonUp("Run")) {
            running = false;
            moveSpeed = 1.5f;
        }

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f) {
            
            if (Input.GetAxisRaw("Horizontal") != 0f) {
                Vector3 collisionPosition = movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.32f, 0f, 0f);
                
                directionX = Input.GetAxisRaw("Horizontal");
                directionY = 0f;

                if (!Physics2D.OverlapCircle(collisionPosition, .1f, stopMove)) {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal")*0.32f, 0f, 0f);
                }  
                interactPointer.position = new Vector3(movePoint.position.x + Input.GetAxisRaw("Horizontal")*0.32f, movePoint.position.y, 0f);
            }

            else if (Input.GetAxisRaw("Vertical") != 0f) {
                Vector3 collisionPosition = movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.32f, 0f);
                
                directionY = Input.GetAxisRaw("Vertical");
                directionX = 0f;

                if (!Physics2D.OverlapCircle(collisionPosition, .1f, stopMove)) {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical")*0.32f, 0f);
                }
                interactPointer.position = new Vector3(movePoint.position.x, movePoint.position.y + Input.GetAxisRaw("Vertical")*0.32f, 0f);
            }
            else {
                running = false;
                walking = false;
            }
        }
        else {
            walking = true;
        }
        animator.SetFloat("DirectionX",directionX);
        animator.SetFloat("DirectionY",directionY);
        animator.SetBool("Walking",walking);
        animator.SetBool("Running",running);
    }

    public bool GetInDialogue() {
        return inDialogue;
    }

    public void SetInDialogue(bool value) {
        inDialogue = value;
    }

    public bool GetInTransition() {
        return inTransition;
    }

    public void SetInTransition(bool value) {
        inTransition = value;
    }
}
