using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptable_item_", menuName = "Item")]
public class Item : ScriptableObject {
    public string key;

    public string m_name;
    public string description;
    public Sprite icon;

    public Sprite analyzeImage;

    public void Analyze() {
        Debug.Log(LocalizationManager.instance.GetLocalizedValue(key, 0));
        Debug.Log(LocalizationManager.instance.GetLocalizedValue(key, 1));
    }
}
