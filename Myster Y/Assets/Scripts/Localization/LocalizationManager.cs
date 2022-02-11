using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance;
    private Dictionary<string,string[]> localizedText;

    [System.NonSerialized]public UnityEvent ChangeLocalization = new UnityEvent();

    private string missingTextString = "Localized text not found";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        LoadLocalizedText("Localization/json_localization_ptbr.json");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            LoadLocalizedText("Localization/json_localization_ptbr.json");
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            LoadLocalizedText("Localization/json_localization_en.json");
        }
    }

    public void LoadLocalizedText(string fileName) {
        localizedText = new Dictionary<string, string[]>();
        string filePath = Path.Combine(Application.streamingAssetsPath,fileName);

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            foreach (LocalizationGroup group in loadedData.groups) {
                foreach (LocalizationItem item in group.items) {
                    localizedText.Add(item.key,item.value);
                }
            }
            

        }
        else {
            Debug.LogError("Cannot find file");
        }
        ChangeLocalization.Invoke();
    }

    public string GetLocalizedValue (string key, int textIndex) {
        string result = missingTextString;

        if (localizedText.ContainsKey(key) && textIndex < localizedText[key].Length) {
            result = localizedText[key][textIndex];
        }

        return result;
    }
}