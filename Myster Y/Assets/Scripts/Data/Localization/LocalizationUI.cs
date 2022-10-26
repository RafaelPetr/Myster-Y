using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationUI : MonoBehaviour {
    public string key;

    void Start() {
        LocalizationManager.ChangeLocalization.AddListener(DefineText);
    }

    void DefineText() {
        Text text = GetComponent<Text>();
        LocalizationText localizedText = LocalizationManager.GetLocalizedText(key);
        
        text.text = localizedText.GetValue();
    }
}