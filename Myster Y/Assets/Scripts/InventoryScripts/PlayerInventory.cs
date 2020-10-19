using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerInventory : MonoBehaviour {
    public static PlayerInventory instance;
    private bool openInventory;

    public List<Item> items = new List<Item>();
    private int itemLimit = 18;

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    public Animator animator;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    public void AddItem(Item item) {
        if (items.Count < itemLimit) {
            items.Add(item);
            if (onInventoryChangedCallback != null) {
                onInventoryChangedCallback.Invoke();
            }
        }
    }

    public void RemoveItem(Item item) {
        items.Remove(item);
        if (onInventoryChangedCallback != null) {
            onInventoryChangedCallback.Invoke();
        }
    }

    public bool FindItem(Item item) {
        return items.Contains(item);
    }

    private void Update() {
        if (Input.GetButtonDown("OpenInventory")) {
            openInventory = !openInventory;
            if (openInventory) {
                animator.SetTrigger("OpenInventory");
                CameraHandler.instance.ZoomIn();
            }
            else {
                animator.SetTrigger("CloseInventory");
                CameraHandler.instance.ZoomOut();
            }
        }
    }
}
