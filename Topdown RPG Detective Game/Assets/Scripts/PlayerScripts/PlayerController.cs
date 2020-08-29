using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;
    public bool inDialogue;
    public bool inTransition;
    public Transform movePoint;
    public Animator animator;

    public LayerMask stopMove;
    public Transform interactPointer;

    private float runSpeed = 1;

    // Start is called before the first frame update
    void Start() {
        movePoint.parent = null;
        interactPointer.parent = null;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Run")) {
            runSpeed = 2;
        }

        if (Input.GetButtonUp("Run")) {
            runSpeed = 1;
        }


        if (!inDialogue && !inTransition) {
            transform.position = Vector3.MoveTowards(transform.position,movePoint.position,moveSpeed*Time.deltaTime *runSpeed);
            if (Vector3.Distance(transform.position, movePoint.position) <= .05f) {

                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                    animator.SetFloat("SpeedX", Input.GetAxisRaw("Horizontal") * runSpeed);
                    animator.SetFloat("SpeedY", 0);
                    animator.SetFloat("IdleDir", Input.GetAxisRaw("Horizontal"));

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.16f, 0f, 0f), .1f, stopMove)) {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.32f, 0f, 0f);
                    }
                    interactPointer.position = new Vector3(movePoint.position.x + Input.GetAxisRaw("Horizontal") * 0.32f, movePoint.position.y, 0f);
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                    animator.SetFloat("SpeedX", 0);
                    animator.SetFloat("SpeedY", Input.GetAxisRaw("Vertical") * runSpeed);
                    animator.SetFloat("IdleDir", Input.GetAxisRaw("Vertical") * 2);

                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.16f, 0f), .1f, stopMove)) {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.32f, 0f);
                    }
                    interactPointer.position = new Vector3(movePoint.position.x, movePoint.position.y + Input.GetAxisRaw("Vertical") * 0.32f, 0f);
                }
                else {
                    animator.SetFloat("SpeedX", 0);
                    animator.SetFloat("SpeedY", 0);
                }

                if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f) {
                    animator.SetBool("Walking", true);
                    if (runSpeed == 2) {
                        animator.SetBool("Running", true);
                    }
                    else {
                        animator.SetBool("Running", false);
                    }
                }
                else {
                    animator.SetBool("Walking", false);
                    animator.SetBool("Running", false);
                }
            }
        }
        else {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
        }
        
    }
}
