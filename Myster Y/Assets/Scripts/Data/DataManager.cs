using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class DataManager : MonoBehaviour {
    public static DataManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        LocalizationManager.Load("Localization/json_localization_en.json");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            LocalizationManager.Load("Localization/json_localization_ptbr.json");
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            LocalizationManager.Load("Localization/json_localization_en.json");
        }
    }

    public string Load(string filePath) {
        string dataAsJson = string.Empty;

        if (File.Exists(filePath)) {
            dataAsJson = File.ReadAllText(filePath);
        }
        else {
            Debug.LogError("Cannot find file");
        }

        return dataAsJson;
    }
}