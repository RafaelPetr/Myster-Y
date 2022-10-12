using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Dialogueable {
    [SerializeField]private Item item;

    public override void ExecuteFunction(string function) {
        if (function == "Accept") {
            if (item != null) {
                Inventory.AddItem(item);
                Inventory.AddItem(item);
                Inventory.AddItem(item);
                gameObject.SetActive(false);
            }
        }
    }
}
