using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueable_metro_test : Dialogueable {
    public Item testItem;

    public override Dialogue DefineDialogue() {
        if (!Inventory.HasItem(testItem)) {
            Inventory.AddItem(testItem);
            return dialogues[0];
        }
        else {
            return dialogues[1];
        }
    }
    
}
