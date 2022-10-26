using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {
    //private Item item;
    private string filePath;
    private string key;

    private LocalizationData localizationData;

    public override void OnInspectorGUI() {
        Item item = (Item)target;

        if (string.IsNullOrEmpty(key)) {
            key = item.name;
        }

        if (string.IsNullOrEmpty(item.GetKey())) {
            key = EditorGUILayout.TextField("Key:", key);
            if (GUILayout.Button("Save Key")) {
                item.SetKey(key);
                item.SetName("");
            }
        }
        else {
            EditorGUILayout.LabelField("Key: " + item.GetKey());
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Name");
                item.SetName(GUILayout.TextArea(item.GetName()));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Description");
                item.SetDescription(GUILayout.TextArea(item.GetDescription()));
            GUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.Label("Icon");
            item.SetIcon((Sprite)EditorGUILayout.ObjectField("",item.GetIcon(),typeof(Sprite),true));

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.Label("Analyze Image");
            item.SetAnalysisImage((Sprite)EditorGUILayout.ObjectField("",item.GetAnalysisImage(),typeof(Sprite),true));

            
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            if (GUILayout.Button("Load Data")) {
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
        LocalizationGroup fileGroup = localizationData.GetGroups().Find(group => group.GetKey() == "items");

        if (fileGroup != null) {
            LocalizationElement fileElement = fileGroup.GetElements().Find(data => data.GetKey() == localizationElement.GetKey());

            if (fileElement != null) {
                fileElement.SetValues(localizationElement.GetValues());
            }
            else {
                fileGroup.AddElement(localizationElement);
            }
        }
        else {
            List<LocalizationElement> valuesList = new List<LocalizationElement>();
            valuesList.Add(localizationElement);
            fileGroup = new LocalizationGroup("items", valuesList);

            localizationData.AddGroup(fileGroup);
        }

        string dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(filePath,dataAsJson);

        LoadData();
    }

    private void RemoveData(Item item) {
        LoadData();

        LocalizationElement localizationElement = BuildLocalizationElement(item);
        LocalizationGroup fileGroup = localizationData.GetGroups().Find(group => group.GetKey() == "items");
        LocalizationElement fileElement = fileGroup.GetElements().Find(data => data.GetKey() == localizationElement.GetKey());;

        if (fileElement != null) {
            fileGroup.RemoveElement(fileElement);

            if (fileGroup.GetElements().Count == 0) {
                localizationData.RemoveGroup(fileGroup);
            }
        }

        string dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(filePath,dataAsJson);

        LoadData();
    }

    private LocalizationElement BuildLocalizationElement(Item item) {
        List<string> valueList = new List<string>();

        valueList.Add(item.GetName());
        valueList.Add(item.GetDescription());

        return new LocalizationElement(item.GetKey(),valueList.ToArray());
    }
}

[CustomEditor(typeof(Flask))]
public class FlaskEditor : ItemEditor {}