using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizationEditor : EditorWindow {
    public LocalizationData localizationData;
    private string filePath;

    [MenuItem ("Window/Localization Editor")]
    static void Init() {
        EditorWindow.GetWindow(typeof(LocalizationEditor)).Show();
    }

    private void OnGUI() {
        if (localizationData != null) {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty,true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save data")) {
                SaveData();
            }
        }

        if (GUILayout.Button("Load data")) {
            LoadData();
        }
    }

    private void LoadData() {
        filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

        if (!string.IsNullOrEmpty(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);
            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }

    private void SaveData() {
        if (!string.IsNullOrEmpty(filePath)) {
            string dataAsJson = JsonUtility.ToJson(localizationData,true);
            File.WriteAllText(filePath,dataAsJson);
        }
    }
}
