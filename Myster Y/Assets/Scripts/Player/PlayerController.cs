using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    private PlayerAnimator animator;

    private float moveSpeed = 1.5f;

    private float directionX;
    private float directionY;

    private float inventory;
    private bool openInventory;
    private bool closeInventory;
    private bool exitInventory;

    private bool walking;
    private bool running;
    private bool inInteraction;
    private bool inTransition;
    private bool inInventory;

    public LayerMask collidable;

    public Transform movePointer;
    public Transform interactPointer;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        movePointer.parent = null;
        interactPointer.parent = null;
        animator = gameObject.AddComponent<PlayerAnimator>();
    }

    private void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, movePointer.position, Time.deltaTime * moveSpeed);
    }

    void Update() {
        ControlMovement();
        ControlInventory();
    }

    private void ControlMovement() {
        if (!BlockMovement()) {
            if (Input.GetButton("Run")) {
                running = true;
                moveSpeed = 2.5f;
            }
            if (Input.GetButtonUp("Run")) {
                running = false;
                moveSpeed = 1.5f;
            }

            if (Vector3.Distance(transform.position, movePointer.position) <= .05f) {
                
                if (Input.GetAxisRaw("Horizontal") != 0f) {
                    Vector3 collisionPosition = movePointer.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.32f, 0f, 0f);
                    
                    directionX = Input.GetAxisRaw("Horizontal");
                    directionY = 0f;

                    if (!Physics2D.OverlapCircle(collisionPosition, .1f, collidable)) {
                        movePointer.position += new Vector3(Input.GetAxisRaw("Horizontal")*0.32f, 0f, 0f);
                    }
                    else {
                        running = false;
                        walking = false;
                    }
                    interactPointer.position = new Vector3(movePointer.position.x + Input.GetAxisRaw("Horizontal")*0.32f, movePointer.position.y, 0f);
                }

                else if (Input.GetAxisRaw("Vertical") != 0f) {
                    Vector3 collisionPosition = movePointer.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.32f, 0f);
                    
                    directionY = Input.GetAxisRaw("Vertical");
                    directionX = 0f;

                    if (!Physics2D.OverlapCircle(collisionPosition, .1f, collidable)) {
                        movePointer.position += new Vector3(0f, Input.GetAxisRaw("Vertical")*0.32f, 0f);
                    }
                    else {
                        running = false;
                        walking = false;
                    }
                    interactPointer.position = new Vector3(movePointer.position.x, movePointer.position.y + Input.GetAxisRaw("Vertical")*0.32f, 0f);
                }

                else {
                    running = false;
                    walking = false;
                }
            }
            else {
                walking = true;
            }
        }
        else {
            running = false;
            walking = false;
        }
    }

    private void ControlInventory() {
        if (!exitInventory) {
            if (inInventory) {
                if (inventory == 0) {
                    inventory = Input.GetAxisRaw("Horizontal");

                    if (Input.GetAxisRaw("Horizontal") != 0f) {
                        openInventory = true;
                    }

                    if (Input.GetButtonDown("Cancel")) {
                        exitInventory = true;
                    }
                }
                else {
                    if (Input.GetButtonDown("Cancel")) {
                        closeInventory = true;
                    }
                }
            }
            else {
                if (Input.GetButtonDown("Inventory")) {
                    directionX = 0;
                    directionY = -1;
                    
                    interactPointer.transform.position = movePointer.transform.position;
                    
                    inInventory = true;
                }
            }
        }
    }

    private bool BlockMovement() {
        if (inInteraction) {
            return true;
        }
        else if (inTransition) {
            return true;
        }
        else if (inInventory) {
            return true;
        }
        return false;
    }

    #region Get and Set Variables Functions
    public bool GetInInteraction() {
        return inInteraction;
    }

    public void SetInInteraction(bool value) {
        inInteraction = value;
    }

    public bool GetInTransition() {
        return inTransition;
    }

    public void SetInTransition(bool value) {
        inTransition = value;
    }

    public bool GetWalking() {
        return walking;
    }

    public bool GetRunning() {
        return running;
    }

    public bool GetInInventory() {
        return inInventory;
    }

    public float GetInventory() {
        return inventory;
    }

    public void SetInventory(float value) {
        inventory = value;
    }

    public bool GetOpenInventory() {
        return openInventory;
    }
    
    public void SetOpenInventory(bool value) {
        openInventory = value;
    }

    public bool GetCloseInventory() {
        return closeInventory;
    }
    
    public void SetCloseInventory(bool value) {
        closeInventory = value;
        inventory = 0f;
    }

    public bool GetExitInventory() {
        return exitInventory;
    }

    public void SetExitInventory(bool value) {
        exitInventory = value;
        inInventory = false;
        inventory = 0f;
    }

    public float GetDirectionX() {
        return directionX;
    }

    public float GetDirectionY() {
        return directionY;
    }
    #endregion
}
