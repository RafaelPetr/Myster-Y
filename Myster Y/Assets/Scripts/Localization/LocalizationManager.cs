using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance;

    private Dictionary<string,string[]> localizedText;

    private bool isReady = false;

    [System.NonSerialized]public UnityEvent ChangeLocalization;

    private string missingTextString = "Localized text not found";

    private void Awake() {
        ChangeLocalization = new UnityEvent();

        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        LoadLocalizedText("localizedText_ptbr.json");
        ChangeLocalization.Invoke();

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            LoadLocalizedText("localizedText_ptbr.json");
            ChangeLocalization.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            LoadLocalizedText("localizedText_en.json");
            ChangeLocalization.Invoke();
        }
    }

    public void LoadLocalizedText(string fileName) {
        localizedText = new Dictionary<string, string[]>();
        string filePath = Path.Combine(Application.streamingAssetsPath,fileName);

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Count; i++) {
                localizedText.Add(loadedData.items[i].key,loadedData.items[i].value);
            }

        }

        else {
            Debug.LogError("Cannot find file");
        }

        isReady = true;
    }

    public string GetLocalizedValue (string key, int textIndex) {
        string result = missingTextString;

        if (localizedText.ContainsKey(key) && textIndex < localizedText[key].Length) {
            result = localizedText[key][textIndex];
        }

        return result;
    }

    public bool GetIsReady() {
        return isReady;
    }
}
