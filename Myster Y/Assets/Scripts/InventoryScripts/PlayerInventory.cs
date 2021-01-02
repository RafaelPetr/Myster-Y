using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public static PlayerInventory instance;
    private Animator animator;

    #region Singleton

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this);
        }
        animator = GetComponent<Animator>();
    }
    #endregion

    public List<InventoryPanel> panels = new List<InventoryPanel>();

    [System.NonSerialized]public bool open;
    [System.NonSerialized]public InventoryPanel activePanel;
    public int toOpen = -1;

    public void AddItem(Item item) {
        foreach (InventoryPanel panel in panels) {
            if (panel.items.Count < panel.itemLimit) {
                panel.items.Add(item);
                break;
            }
        }
    }

    public void RemoveItem(Item item) {
        foreach (InventoryPanel panel in panels) {
            if (panel.items.Contains(item)) {
                panel.items.Remove(item);
                break;
            }
        }
    }

    public bool FindItem(Item item) {
        foreach (InventoryPanel panel in panels) {
            if (panel.items.Contains(item)) {
                return true;
            }
        }
        return false;
    }

    private void Update() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Down") && toOpen != -1) {
            animator.SetFloat("InventoryPanel",toOpen);
            animator.Play("OpenInventoryPanel", 0);
            toOpen = -1;
        }
        if (open) {
            if (activePanel == null) {
                if (Input.GetButtonDown("Left")) {
                    OpenPanel(0);
                }
                if (Input.GetButtonDown("Right")) {
                    OpenPanel(1);
                }
            }
            if (Input.GetButtonDown("Back")) {
                if (activePanel != null) {
                    ClosePanel();
                }
                else {
                    CloseInventory();
                }
            }
        }
        else {
            if (Input.GetButtonDown("OpenInventory")) {
                OpenInventory();
            }
        }
    }

    public void OpenPanel(int index) {
        if (index == animator.GetFloat("InventoryPanel")) {
            float animationTime = Mathf.Lerp(0, 1, 1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            animator.SetFloat("InventoryPanel",index);
            animator.Play("OpenInventoryPanel", 0, animationTime);
        }
        else {
            toOpen = index;
        }
        activePanel = panels[index];
        InventoryUI.instance.UpdateUI(index);
    }

    private void ClosePanel() {
        if (panels.IndexOf(activePanel) == animator.GetFloat("InventoryPanel")) {
            float animationTime = Mathf.Lerp(0, 1, 1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            animator.Play("CloseInventoryPanel", 0, animationTime);
        }
        activePanel = null;
        InventoryUI.instance.UpdateUI(-1);
        toOpen = -1;
    }

    private void OpenInventory() {
        open = true;
        CameraHandler.instance.ZoomIn();
        animator.SetTrigger("OpenInventory");
        animator.SetFloat("IdleDir",-2);
    }

    private void CloseInventory() {
        open = false;
        CameraHandler.instance.ZoomOut();
        animator.SetTrigger("CloseInventory");
    }
}
