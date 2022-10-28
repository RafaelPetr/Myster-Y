using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class LocalizationManager {
    private static bool ready;

    private static Dictionary<string, LocalizationDialogue> localizedDialogues = new Dictionary<string, LocalizationDialogue>();
    private static Dictionary<string, LocalizationItem> localizedItems = new Dictionary<string, LocalizationItem>();
    private static Dictionary<string, LocalizationText> localizedTexts = new Dictionary<string, LocalizationText>();

    [System.NonSerialized]public static UnityEvent ChangeLocalization = new UnityEvent();

    public static void Load(string filePath) {
        string dataAsJson = DataManager.instance.Load(Application.streamingAssetsPath + filePath);

        if (dataAsJson != string.Empty) {
            LocalizationData localizedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            LoadDictionaries(localizedData);

            ChangeLocalization.Invoke();
            ready = true;
        }
    }

    private static void LoadDictionaries(LocalizationData data) {
        localizedDialogues.Clear();
        localizedItems.Clear();
        localizedTexts.Clear();

        foreach (LocalizationDialogue dialogue in data.GetDialogues()) {
            if (!localizedDialogues.ContainsKey(dialogue.GetKey())) {
                localizedDialogues.Add(dialogue.GetKey(), dialogue);
            }
        }

        foreach (LocalizationItem item in data.GetItems()) {
            if (!localizedItems.ContainsKey(item.GetKey())) {
                localizedItems.Add(item.GetKey(), item);
            }
        }

        foreach (LocalizationText text in data.GetTexts()) {
            if (!localizedTexts.ContainsKey(text.GetKey())) {
                localizedTexts.Add(text.GetKey(), text);
            }
        }
    }

    #region Getters

        public static bool GetReady() {
            return ready;
        }

        public static LocalizationDialogue GetLocalizedDialogue(string key) {
            if (localizedDialogues.ContainsKey(key)) {
                return localizedDialogues[key];
            }

            return null;
        }

        public static LocalizationItem GetLocalizedItem(string key) {
            if (localizedItems.ContainsKey(key)) {
                return localizedItems[key];
            }

            return null;
        }

        public static LocalizationText GetLocalizedText(string key) {
            if (localizedTexts.ContainsKey(key)) {
                return localizedTexts[key];
            }

            return null;
        }

    #endregion
}