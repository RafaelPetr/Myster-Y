using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    #region Singleton
    public static InventoryUI instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }
    
    #endregion

    InventorySlot[] slots;

    public Animator animator;

    private PlayerInventory inventory;

    private Transform activePanel;

    [System.NonSerialized]public bool open = false;
    private int UI_index = -1;

    [System.NonSerialized]public InventorySlot selectedSlot;

    void Start() {
        inventory = PlayerInventory.instance;
    }

    private void Update() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Down") && UI_index != -1) {
            animator.SetTrigger("OpenInventory");
            animator.SetFloat("InventoryPlace",UI_index);
        }

        if (open) {
            if (Input.GetButtonDown("Left")) {
                OpenInventory(0);
            }
            if (Input.GetButtonDown("Right")) {
                OpenInventory(1);
            }
            if (Input.GetButtonDown("Down")) {
                OpenInventory(2);
            }
            if (UI_index == -1) {
                if (Input.GetButtonDown("Back")) {
                    ExitInventory();
                }
            }
            else {
                if (Input.GetButtonDown("Back")) {
                    float animationTime = Mathf.Lerp(0, 1, 1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    animator.Play("CloseInventoryPlace", 0, animationTime);
                    UI_index = -1;
                    UpdateUI();
                }
            }
        }

        else {
            if (Input.GetButtonDown("OpenInventory")) {
                open = true;
                if (open) {
                    CameraHandler.instance.ZoomIn();
                    animator.SetTrigger("StartInventory");
                    animator.SetFloat("IdleDir",-2);
                }
            }
        }

        if (Input.GetButtonDown("Interact") && selectedSlot != null) {
            selectedSlot.AnalyseItem();
        }
    }

    void OpenInventory(int index) {
        if (UI_index == -1) {
            UI_index = index;
            UpdateUI();
        }
    }

    void ExitInventory() {
        open = false;
        CameraHandler.instance.ZoomOut();
        animator.SetTrigger("ExitInventory");
        UI_index = -1;
        UpdateUI();
    }

    void UpdateUI() {

        if (UI_index != -1) {
            
            if (activePanel != null) {
                activePanel.gameObject.SetActive(false);
            }

            activePanel = GetComponent<Transform>().GetChild(UI_index);
            activePanel.gameObject.SetActive(true);

            Transform itemsParent = activePanel.GetChild(0);

            itemsParent.GetChild(0).GetComponent<Button>().Select();
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();

            int itemArrayPosition = 0;

            for (int i = 0; i < UI_index; i++) {
                itemArrayPosition += inventory.panelsLimit[i];
            }

            if (inventory.items.Count >= itemArrayPosition) {
                for (int i = 0; i < slots.Length; i++) {
                    if (i < inventory.items.Count) {
                        slots[i].AddItem(inventory.items[i+itemArrayPosition]);
                    }
                    else {
                        slots[i].ClearSlot();
                    }
                }
            }
        }
        
        else {
            if (activePanel != null) {
                activePanel.gameObject.SetActive(false);
                selectedSlot = null;
            }
        }
    }
}
