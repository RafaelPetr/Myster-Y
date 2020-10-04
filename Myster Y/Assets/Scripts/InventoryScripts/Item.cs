using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    public string name;
    public Sprite frameImage;
    public ItemObject itemObject;

    public void AnalyseObject() {
        itemObject.Analyse();
    }

    public void UseItem() {

    }

    public void ShowItem() {

    }

    public void RemoveFromInventory() {
        
    }


}
