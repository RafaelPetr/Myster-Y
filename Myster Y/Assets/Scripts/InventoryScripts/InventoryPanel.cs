using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/InventoryPanel")]
public class InventoryPanel : ScriptableObject {

    public List<Item> items = new List<Item>();
    public int itemLimit;
    
}
