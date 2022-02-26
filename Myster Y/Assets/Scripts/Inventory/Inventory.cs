using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory instance;
    private List<Item> items = new List<Item>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public Item GetItem(int index) {
        return items[index];
    }

    public List<Item> GetAllItems() {
        return items;
    }

    public void AddItem(Item item) {
        if (items.Count < 12) {
            items.Add(item);
        }
        else {
            Debug.Log("Your inventory is full");
        }
    }

    public bool FindItem(Item item) {
        return items.Contains(item);
    }

    public void RemoveItem(Item item) {
        items.Remove(item);
    }
}
