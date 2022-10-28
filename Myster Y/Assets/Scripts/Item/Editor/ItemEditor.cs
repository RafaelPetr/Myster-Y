using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {
    private string key;

    private string dataPath;
    private string localizationPath;

    private DataItemList dataItemList;
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

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            if (GUILayout.Button("Add Description")) {
                item.AddDescription(string.Empty);
            }

            for (int i = 0; i < item.GetDescription().Count; i++) {
                EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

                GUILayout.BeginHorizontal();
                    GUILayout.Label("Description " + (i+1));
                    item.SetDescription(i, GUILayout.TextArea(item.GetDescription(i)));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Remove Description")) {
                    item.RemoveDescription(i);
                }
            }

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.Label("Icon");
            item.SetIcon((Sprite)EditorGUILayout.ObjectField("",item.GetIcon(),typeof(Sprite),true));

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.Label("Analyze Image");
            item.SetAnalysisImage((Sprite)EditorGUILayout.ObjectField("",item.GetAnalysisImage(),typeof(Sprite),true));

            
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            if (GUILayout.Button("Load Data")) {
                LoadData(item);
            }

            GUILayout.BeginHorizontal();

                if (GUILayout.Button("Save Data")) {
                    SaveData(item);
                }
                if (GUILayout.Button("Remove Data")) {
                    RemoveData(item);
                }

            GUILayout.EndHorizontal();
        }
        
        EditorUtility.SetDirty(item);
    }

    private void LoadData() {
        string dataAsJson;

        /*if (string.IsNullOrEmpty(filePath)) {
            filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
        }*/

        dataPath = Application.streamingAssetsPath + "/Item/json_itemData.json";
        dataAsJson = File.ReadAllText(dataPath);
        dataItemList = JsonUtility.FromJson<DataItemList>(dataAsJson);

        localizationPath = Application.streamingAssetsPath + "/Localization/json_localization_ptbr.json";
        dataAsJson = File.ReadAllText(localizationPath);
        localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
    }

    private void LoadData(Item item) {
        LoadData();

        DataItem dataItem = dataItemList.GetItems().Find(data => data.GetKey() == item.GetKey());

        if (dataItem != null) {
            item.LoadData(dataItem);
        }
    }

    private void SaveData(Item item) {
        string dataAsJson;

        LoadData();
        RemoveData(item);

        DataItem fileData = new DataItem(item);
        dataItemList.AddItem(fileData);
        dataAsJson = JsonUtility.ToJson(dataItemList,true);
        File.WriteAllText(dataPath,dataAsJson);

        LocalizationItem fileLocalization = new LocalizationItem(item);
        localizationData.AddItem(fileLocalization);
        dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(localizationPath,dataAsJson);

        LoadData();
    }

    private void RemoveData(Item item) {
        LoadData();

        DataItem fileData = dataItemList.GetItems().Find(data => data.GetKey() == item.GetKey());
        LocalizationItem fileLocalization = localizationData.GetItems().Find(data => data.GetKey() == item.GetKey());

        if (fileData != null) {
            dataItemList.RemoveItem(fileData);
        }

        if (fileLocalization != null) {
            localizationData.RemoveItem(fileLocalization);
        }

        string dataAsJson = JsonUtility.ToJson(dataItemList,true);
        File.WriteAllText(dataPath,dataAsJson);

        dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(localizationPath,dataAsJson);

        LoadData();
    }
}

[CustomEditor(typeof(Flask))]
public class FlaskEditor : ItemEditor {}