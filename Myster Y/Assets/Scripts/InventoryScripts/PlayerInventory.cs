using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerInventory : MonoBehaviour {
    
    #region Singleton

    public static PlayerInventory instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            instance.onInventoryUpdateCallback = null;
            instance.animator = this.animator;
            Destroy(this);
        }
    }

    #endregion

    [System.NonSerialized]public bool open;
    [System.NonSerialized]public int UI_index = -1;
    private bool inventoryLeft;
    private bool inventoryRight;

    public List<Item> items = new List<Item>();
    private int itemLimit = 18;

    public delegate void onInventoryUpdate();
    public onInventoryUpdate onInventoryUpdateCallback;

    public Animator animator;

    public void AddItem(Item item) {
        if (items.Count < itemLimit) {
            items.Add(item);
        }
    }

    public void RemoveItem(Item item) {
        items.Remove(item);
    }

    public bool FindItem(Item item) {
        return items.Contains(item);
    }

    private void Update() {
        if (Input.GetButtonDown("OpenInventory")) {
            open = !open;
            if (open) {
                CameraHandler.instance.ZoomIn();
                animator.SetTrigger("StartInventory");
                animator.SetFloat("IdleDir",-2);
            }
            else {
                CameraHandler.instance.ZoomOut();
                UI_index = -1;
                onInventoryUpdateCallback.Invoke();
                animator.SetTrigger("ExitInventory");
            }
        }

        if (Input.GetButtonDown("Left") && open) {
            UI_index = 0;
            onInventoryUpdateCallback.Invoke();
        }
        if (Input.GetButtonDown("Right")  && open) {
            UI_index = 1;
            onInventoryUpdateCallback.Invoke();
        }
        if (Input.GetButtonDown("Down") && open) {
            UI_index = 2;
            onInventoryUpdateCallback.Invoke();
        }
    }
}
