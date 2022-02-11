using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour {
    public string key;

    void Start() {
        LocalizationManager.instance.ChangeLocalization.AddListener(DefineText);
    }

    void DefineText() {
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key,1);
    }
}