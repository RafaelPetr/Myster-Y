using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class LocalizationManager : MonoBehaviour {
    public static LocalizationManager instance;
    private bool isReady;

    private Dictionary<string,string[]> localizedText;

    [System.NonSerialized]public UnityEvent ChangeLocalization = new UnityEvent();

    private string missingTextString = "Localized text not found";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
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

    private void LoadLocalizedText(string fileName) {
        localizedText = new Dictionary<string, string[]>();
        string filePath = Path.Combine(Application.streamingAssetsPath,fileName);

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            foreach (LocalizationGroup group in loadedData.groups) {
                foreach (LocalizationElement element in group.elements) {
                    localizedText.Add(element.key,element.values);
                }
            }
        }
        else {
            Debug.LogError("Cannot find file");
        }
        ChangeLocalization.Invoke();
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