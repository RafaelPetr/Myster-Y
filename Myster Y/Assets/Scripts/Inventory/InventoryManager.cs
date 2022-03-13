using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance;
    private bool inTransition;

    private int activePanel;
    
    [SerializeField]private Transform inventoryUI;
    private List<Transform> inventoryPanels = new List<Transform>();
    private ItemSlot[] slots;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        for (int i = 0; i < inventoryUI.childCount; i++) {
            inventoryPanels.Add(inventoryUI.GetChild(i));
        }
        SceneController.instance.StartLoadEvent.AddListener(OnStartLoad);
        SceneController.instance.EndLoadEvent.AddListener(OnEndLoad);
    }

    public void Open(int index) {
        if (!inTransition) {
            Close();

            if (index < 0) {
                index = 0;
            }

            activePanel = index;

            inventoryPanels[index].gameObject.SetActive(true);

            slots = inventoryPanels[index].GetComponentsInChildren<ItemSlot>();
            slots[0].button.Select();

            int startIndex = index*6; //Start of the interval from the inventory selected by the panel
            int endIndex = (index + 1)*6; //End of the interval from the inventory selected by the panel

            for (int i = startIndex; i < endIndex; i++) {
                ItemSlot currentSlot = slots[6 - (endIndex - i)];

                if (i < Inventory.instance.GetAllItems().Count) {
                    currentSlot.AddItem(Inventory.instance.GetItem(i));
                }
                else {
                    currentSlot.Clear();
                }
            }
        }
    }

    public void OpenLastPanelClosed() {
        Open(activePanel);
    }

    public void Close() {
        EventSystem.current.SetSelectedGameObject(null);
        foreach (Transform panel in inventoryPanels) {
            panel.gameObject.SetActive(false);
        }
    }

    private void OnStartLoad() {
        Close();
        inTransition = true;
    }

    private void OnEndLoad() {
        inTransition = false;
    }
    
}