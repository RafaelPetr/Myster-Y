using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public Transform itemsParent;
    InventorySlot[] slots;

    private PlayerInventory inventory;

    void Start() {
        inventory = PlayerInventory.instance;
        inventory.onInventoryChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }
}
