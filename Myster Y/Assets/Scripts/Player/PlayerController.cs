using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    private PlayerAnimator animator;

    private float moveSpeed = 1.5f;

    private float directionX;
    private float directionY = -1;

    private float inventoryPanel;
    private bool enterInventoryTrigger;
    private bool openInventoryTrigger;
    private bool closeInventoryTrigger;
    private bool exitInventoryTrigger;

    private bool walking;
    private bool running;
    private bool inInteraction;
    private bool inTransition;
    private bool inInventory;

    public LayerMask collidable;

    public Transform movePointer;
    public InteractPointer interactPointer;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        movePointer.parent = null;
        animator = gameObject.AddComponent<PlayerAnimator>();

        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.5f,0.5f,0);
        collider.offset = new Vector3(0f,0.09f,0);

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

        Sortable sortable = gameObject.AddComponent<Sortable>();
        sortable.SetMovement(true);
        sortable.SetPriority(1);
    }

    private void FixedUpdate() {
        if (!BlockMovement()) {
            transform.position = Vector3.MoveTowards(transform.position, movePointer.position, Time.deltaTime * moveSpeed);
        }
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
                    Vector3 collisionPosition = movePointer.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.5f, 0f, 0f);
                    
                    directionX = Input.GetAxisRaw("Horizontal");
                    directionY = 0f;

                    if (!Physics2D.OverlapCircle(collisionPosition, .1f, collidable)) {
                        movePointer.position += new Vector3(Input.GetAxisRaw("Horizontal")*0.5f, 0f, 0f);
                    }
                    else {
                        running = false;
                        walking = false;
                    }
                    interactPointer.Move(new Vector3(movePointer.position.x + Input.GetAxisRaw("Horizontal")*0.5f, movePointer.position.y - 0.5f, 0f));
                }

                else if (Input.GetAxisRaw("Vertical") != 0f) {
                    Vector3 collisionPosition = movePointer.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.5f, 0f);
                    
                    directionY = Input.GetAxisRaw("Vertical");
                    directionX = 0f;

                    if (!Physics2D.OverlapCircle(collisionPosition, .1f, collidable)) {
                        movePointer.position += new Vector3(0f, Input.GetAxisRaw("Vertical")*0.5f, 0f);
                    }
                    else {
                        running = false;
                        walking = false;
                    }
                    interactPointer.Move(new Vector3(movePointer.position.x, movePointer.position.y - 0.5f + Input.GetAxisRaw("Vertical")*0.5f, 0f));
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
            moveSpeed = 1.5f;
        }
    }

    private void ControlInventory() {
        if (!exitInventoryTrigger) {
            if (inInventory) {
                if (inventoryPanel == 0) {
                    inventoryPanel = Input.GetAxisRaw("Horizontal");

                    if (Input.GetAxisRaw("Horizontal") != 0f) {
                        openInventoryTrigger = true;
                        InventoryManager.instance.Open((int)inventoryPanel);
                    }

                    if (Input.GetButtonDown("Cancel")) {
                        exitInventoryTrigger = true;
                        CameraManager.instance.SetZoom(false);
                    }
                }
                else {
                    if (Input.GetButtonDown("Cancel") && !openInventoryTrigger) {
                        closeInventoryTrigger = true;
                        InventoryManager.instance.Close();
                    }
                }
            }
            else {
                if (Input.GetButtonDown("inventoryPanel")) {
                    directionX = 0;
                    directionY = -1;
                    
                    interactPointer.Move(movePointer.transform.position);
                    
                    enterInventoryTrigger = true;
                    inInventory = true;

                    CameraManager.instance.SetZoom(true);
                }
            }
        }
    }

    private bool BlockMovement() {
        return inInteraction || inTransition || inInventory;
    }

    #region Getters

        public float GetDirectionX() {
            return directionX;
        }

        public float GetDirectionY() {
            return directionY;
        }
        
        public bool GetInInteraction() {
            return inInteraction;
        }

        public bool GetInTransition() {
            return inTransition;
        }

        public bool GetWalking() {
            return walking;
        }

        public bool GetRunning() {
            return running;
        }

        #region Inventory

            public bool GetInInventory() {
                return inInventory;
            }

            public float GetInventoryPanel() {
                return inventoryPanel;
            }

            public bool GetEnterInventoryTrigger() {
                return enterInventoryTrigger;
            }

            public bool GetOpenInventoryTrigger() {
                return openInventoryTrigger;
            }

            public bool GetCloseInventoryTrigger() {
                return closeInventoryTrigger;
            }

            public bool GetExitInventoryTrigger() {
                return exitInventoryTrigger;
            }

        #endregion

    #endregion

    #region Setters

        public void SetDirectionX(float value) {
            directionX = value;
        }

        public void SetDirectionY(float value) {
            directionY = value;
        }

        public void SetInInteraction(bool value) {
            inInteraction = value;
        }

        public void SetInTransition(bool value) {
            inTransition = value;
        }

        #region Inventory

            public void SetInventoryPanel(float value) {
                inventoryPanel = value;
            }

            public void SetEnterInventoryTrigger(bool value) {
                enterInventoryTrigger = value;
            }

            public void SetOpenInventoryTrigger(bool value) {
                openInventoryTrigger = value;
            }

            public void SetCloseInventoryTrigger(bool value) {
                closeInventoryTrigger = value;
                inventoryPanel = 0f;
            }

            public void SetExitInventoryTrigger(bool value) {
                exitInventoryTrigger = value;
                inInventory = false;
                inventoryPanel = 0f;
            }

        #endregion

    #endregion
}
