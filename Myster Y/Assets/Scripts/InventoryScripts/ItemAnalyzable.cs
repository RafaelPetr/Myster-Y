using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/ItemAnalyzable")]
public class ItemAnalyzable : ScriptableObject {
    public Sprite sprite;

    public void Analyze() {
        Debug.Log("Some cool function");
    }

}
