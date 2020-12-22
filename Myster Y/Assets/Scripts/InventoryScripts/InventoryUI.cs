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
    private PlayerInventory inventory;
    private Transform activePanel;
    [System.NonSerialized]public InventorySlot selectedSlot;

    void Start() {
        inventory = PlayerInventory.instance;
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && selectedSlot != null) {
            selectedSlot.AnalyseItem();
        }
    }

    public void UpdateUI(int index) { //index = panel index on list
        if (index == -1) { //-1 closes the panel
            activePanel.gameObject.SetActive(false);
            activePanel = null;
            selectedSlot = null;
        }

        else {
            activePanel = GetComponent<Transform>().GetChild(index);
            activePanel.gameObject.SetActive(true);

            Transform itemsParent = activePanel.GetChild(0);

            itemsParent.GetChild(0).GetComponent<Button>().Select();
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();

            for (int i = 0; i < slots.Length; i++) {
                if (i < inventory.activePanel.items.Count) {
                    slots[i].AddItem(inventory.activePanel.items[i]);
                }
                else {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}
