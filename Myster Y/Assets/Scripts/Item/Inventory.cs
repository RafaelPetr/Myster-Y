using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory {
    private static List<Item> items = new List<Item>();

    public static Item GetItem(int index) {
        return items[index];
    }

    public static Item GetItem(string key) {
        foreach (Item item in items) {
            if (item.key == key) {
                return item;
            }
        }
        return null;
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

    public static bool HasItem(Item item) {
        return items.Contains(item);
    }

    public static bool HasItem(string key) {
        foreach (Item item in items) {
            if (item.key == key) {
                return true;
            }
        }
        return false;
    }

    public static void RemoveItem(Item item) {
        items.Remove(item);
    }
}
