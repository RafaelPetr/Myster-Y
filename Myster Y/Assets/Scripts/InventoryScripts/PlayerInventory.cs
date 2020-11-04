using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerInventory : MonoBehaviour {
    
    #region Singleton

    public static PlayerInventory instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(this);
        }
    }

    #endregion

    public List<Item> items = new List<Item>();
    [System.NonSerialized]public int[] panelsLimit = new int[3]{6,6,3};
    private int itemLimit = 15;

    public void AddItem(Item item) {
        if (items.Count < itemLimit) {
            items.Add(item);
        }
    }

    public void RemoveItem(Item item) {
        items.Remove(item);
    }

    public bool FindItem(Item item) {
        return items.Contains(item);
    }
}
