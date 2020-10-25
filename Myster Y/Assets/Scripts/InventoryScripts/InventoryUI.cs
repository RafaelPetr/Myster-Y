using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public Transform inventoryTopcoatRight;
    InventorySlot[] slots;

    private PlayerInventory inventory;

    private Transform activePanel;

    void Start() {
        inventory = PlayerInventory.instance;
        inventory.onInventoryUpdateCallback += UpdateUI;
    }

    void UpdateUI() {

        if (inventory.UI_index != -1) {
            
            if (activePanel != null) {
                activePanel.gameObject.SetActive(false);
            }

            activePanel = GetComponent<Transform>().GetChild(inventory.UI_index);
            activePanel.gameObject.SetActive(true);
            Transform itemsParent = activePanel.GetChild(0);
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();

            for (int i = 0; i < slots.Length; i++) {
                if (i < inventory.items.Count) {
                    slots[i].AddItem(inventory.items[i]);
                }
                else {
                    slots[i].ClearSlot();
                }
            }
        }
        
        else {
            if (activePanel != null) {
                activePanel.gameObject.SetActive(false);
            }
        }
    }
}
