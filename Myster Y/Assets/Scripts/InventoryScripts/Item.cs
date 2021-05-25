using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    public string name;
    public Sprite icon;
    public ItemAnalyzable analyzable;

    public void PickUp() {
        PlayerInventory.instance.AddItem(this);
    }

    public void Use() {
        AnalyzeManager.instance.StartAnalyze(this);
    }
}
