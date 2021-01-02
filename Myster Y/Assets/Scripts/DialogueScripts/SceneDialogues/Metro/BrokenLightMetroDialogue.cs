using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLightMetroDialogue : DialogueSource {
    public Dialogue defaultDialogue;
    public Dialogue keyDialogue;
    public Item keyItem;

    public override Dialogue DefineDialogue() {
        if (PlayerInventory.instance.FindItem(keyItem)) {
            return keyDialogue;
        }
        else {
            keyItem.PickUp();
            return defaultDialogue;
        }
    }


}
