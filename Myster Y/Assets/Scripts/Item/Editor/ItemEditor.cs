using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {
    public Item item;
    private string filePath;
    private string key;

    private LocalizationData localizationData;

    public override void OnInspectorGUI() {
        item = (Item)target;

        if (string.IsNullOrEmpty(key)) {
            key = item.name;
        }

        if (string.IsNullOrEmpty(item.key)) {
            key = EditorGUILayout.TextField("Key:",key);
            if (GUILayout.Button("Save Key")) {
                item.key = key;
                item.m_name = "";
            }
        }
        else {
            EditorGUILayout.LabelField("Key: " + item.key);
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Name");
                item.m_name = GUILayout.TextArea(item.m_name);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Description");
                item.description = GUILayout.TextArea(item.description);
            GUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.Label("Icon");
            item.icon = (Sprite)EditorGUILayout.ObjectField("",item.icon,typeof(Sprite),true);

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.Label("Analyze Image");
            item.analysisImage = (Sprite)EditorGUILayout.ObjectField("",item.analysisImage,typeof(Sprite),true);

            
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            if (GUILayout.Button("Load Data")) {
                filePath = null;
                LoadData();
            }

            GUILayout.BeginHorizontal();

                //if (localizationData != null) {
                    if (GUILayout.Button("Save Data")) {
                        SaveData(item);
                    }
                    if (GUILayout.Button("Remove Data")) {
                        RemoveData(item);
                    }
                //}

            GUILayout.EndHorizontal();
        }
        
        EditorUtility.SetDirty(item);
    }

    private void LoadData() {
        filePath = Application.streamingAssetsPath + "/Localization/json_localization_ptbr.json";

        if (string.IsNullOrEmpty(filePath)) {
            filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
        }

        string dataAsJson = File.ReadAllText(filePath);
        localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
    }

    private void SaveData(Item item) {
        LoadData();

        LocalizationElement localizationElement = BuildLocalizationElement(item);
        LocalizationGroup fileGroup = localizationData.groups.Find(group => group.key == "items");

        if (fileGroup != null) {
            LocalizationElement fileElement = fileGroup.elements.Find(data => data.key == localizationElement.key);

            if (fileElement != null) {
                fileElement.values = localizationElement.values;
            }
            else {
                fileGroup.elements.Add(localizationElement);
            }
        }
        else {
            List<LocalizationElement> valuesList = new List<LocalizationElement>();
            valuesList.Add(localizationElement);
            fileGroup = new LocalizationGroup("items", valuesList);

            localizationData.groups.Add(fileGroup);
        }

        string dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(filePath,dataAsJson);

        LoadData();
    }

    private void RemoveData(Item item) {
        LoadData();

        LocalizationElement localizationElement = BuildLocalizationElement(item);
        LocalizationGroup fileGroup = localizationData.groups.Find(group => group.key == "items");
        LocalizationElement fileElement = fileGroup.elements.Find(data => data.key == localizationElement.key);;

        if (fileElement != null) {
            fileGroup.elements.Remove(fileElement);

            if (fileGroup.elements.Count == 0) {
                localizationData.groups.Remove(fileGroup);
            }
        }

        string dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(filePath,dataAsJson);

        LoadData();
    }

    private LocalizationElement BuildLocalizationElement(Item item) {
        List<string> valueList = new List<string>();

        valueList.Add(item.m_name);
        valueList.Add(item.description);

        return new LocalizationElement(item.key,valueList.ToArray());
    }
}

[CustomEditor(typeof(Flask))]
public class FlaskEditor : ItemEditor {}