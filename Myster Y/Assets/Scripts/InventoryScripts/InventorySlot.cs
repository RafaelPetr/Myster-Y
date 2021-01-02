using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, ISelectHandler {
    Item item;

    public Image icon;

    public void OnSelect(BaseEventData eventData) {
        InventoryUI.instance.selectedSlot = this;
    }

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void AnalyseItem() {
        if (item != null) {
            AnalyseManager.instance.StartAnalyse(item);
        }
    }
}
