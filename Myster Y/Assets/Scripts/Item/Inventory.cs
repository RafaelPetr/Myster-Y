using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory {
    private static List<Item> items = new List<Item>();

    public static Item GetItem(int index) {
        return items[index];
    }

    public static List<Item> GetAllItems() {
        return items;
    }

    public static void AddItem(Item item) {
        if (items.Count < 12) {
            items.Add(item);
        }
        else {
            Debug.Log("Your inventory is full");
        }
    }

    public static bool FindItem(Item item) {
        return items.Contains(item);
    }

    public static void RemoveItem(Item item) {
        items.Remove(item);
    }
}
