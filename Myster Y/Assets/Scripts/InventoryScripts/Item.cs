using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    public string name;
    public Sprite icon;
    public ItemAnalysable itemAnalysable;

    public void PickUp() {
        PlayerInventory.instance.AddItem(this);
    }

    public void AnalyseItem() {
        itemAnalysable.Analyse();
    }

    public void UseItem() {

    }

    public void RemoveFromInventory() {
        
    }


}
