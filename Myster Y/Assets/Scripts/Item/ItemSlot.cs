using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    private Item item;

    [System.NonSerialized]public Button button;
    public Image icon;

    private void Awake() {
        button = GetComponent<Button>();
    }

    private void OnEnable() {
        button.onClick.AddListener(UseItem);
    }

    private void UseItem() {
        if (item != null) {
            InventoryManager.instance.Close();
            item.Analyze();
        }
    }

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void Clear() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
