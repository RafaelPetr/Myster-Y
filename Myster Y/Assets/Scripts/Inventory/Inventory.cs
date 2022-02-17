using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory instance;
    private List<Item> items;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void AddItem(Item item) {
        items.Add(item);
    }
}
