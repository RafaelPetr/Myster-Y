using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor {

    private int tabIndex;

    private string keyString;
    private string filePath;

    private LocalizationData localizationData;

    public override void OnInspectorGUI() {

        Dialogue dialogue = (Dialogue)target;

        if (string.IsNullOrEmpty(keyString)) {
            keyString = dialogue.name;
        }

        if (string.IsNullOrEmpty(dialogue.key)) {
            keyString = EditorGUILayout.TextField("Key:",keyString);
            if (GUILayout.Button("Save Key")) {
                dialogue.key = keyString;
            }
        }
        else {
            EditorGUILayout.LabelField("Key: " + dialogue.key);

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            
            tabIndex = GUILayout.Toolbar(tabIndex, new string[]{"Sentences","Choices","Build"});

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            switch(tabIndex) {
                case 0:
                    SentencesTab(dialogue);
                    break;
                case 1:
                    ChoicesTab(dialogue);
                    break;
                case 2:
                    BuildTab(dialogue);
                    break;
            }
        }
    } 

    private void SentencesTab(Dialogue dialogue) {
        for (int i = 0; i < dialogue.sentences.Count; i++) {
            if (dialogue.sentences[i].character != null) {
                GUILayout.Label("Text");
                dialogue.sentences[i].text = GUILayout.TextArea(dialogue.sentences[i].text);
            }
            GUILayout.BeginHorizontal();
                dialogue.sentences[i].character = (DialogueCharacter)EditorGUILayout.ObjectField("",dialogue.sentences[i].character,typeof(DialogueCharacter));
                if (GUILayout.Button("Delete Element")) {
                    dialogue.RemoveSentence(i);
                }
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
        }

        GUILayout.Label("Sentences: " + dialogue.sentences.Count.ToString());

        if (GUILayout.Button("Add Sentence")) {
            dialogue.AddSentence();
        }
    }

    private void ChoicesTab(Dialogue dialogue) {
        for (int i = 0; i < dialogue.choices.Count; i++) {
            for (int j = 0; j < dialogue.choices[i].options.Count; j++) {
                GUILayout.BeginHorizontal();
                    dialogue.choices[i].options[j] = GUILayout.TextArea(dialogue.choices[i].options[j],GUILayout.Width(170));
                    if (GUILayout.Button("Remove Option")) {
                        dialogue.choices[i].RemoveOption(j);
                    }
                GUILayout.EndHorizontal();
            }
            dialogue.choices[i].character = (DialogueCharacter)EditorGUILayout.ObjectField("",dialogue.choices[i].character,typeof(DialogueCharacter));
            GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add Option")) {
                    dialogue.choices[i].AddOption();
                }
                
                if (GUILayout.Button("Delete Element")) {
                    dialogue.RemoveChoice(i);
                }
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
        }

        GUILayout.Label("Choices: " + dialogue.choices.Count.ToString());

        if (GUILayout.Button("Add Choice")) {
            dialogue.AddChoice();
        }
    }

    private void BuildTab(Dialogue dialogue) {
        for (int i = 0; i < dialogue.elementsOrder.Count; i++) {
            dialogue.elementsOrder[i] = EditorGUILayout.Popup(dialogue.elementsOrder[i], dialogue.elementTypes);
        }

        EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);


            if (GUILayout.Button("Load Data")) {
                filePath = null;
                LoadData();
            }

        GUILayout.BeginHorizontal();

            if (localizationData != null) {
                if (GUILayout.Button("Save Data")) {
                    SaveData(dialogue);
                }
                if (GUILayout.Button("Remove Data")) {
                    RemoveData(dialogue);
                }
            }

        GUILayout.EndHorizontal();
    }

    private void LoadData() {
        if (string.IsNullOrEmpty(filePath)) {
            filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
        }
        string dataAsJson = File.ReadAllText(filePath);
        localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
    }

    private void SaveData(Dialogue dialogue) {

        LocalizationItem localizationItem = BuildLocalizationItem(dialogue);

        LocalizationItem fileItem = localizationData.items.Find(data => data.key == localizationItem.key);;

        if (fileItem != null) {
            fileItem.value = localizationItem.value;
        }
        else {
            localizationData.items.Add(localizationItem);
        }

        string dataAsJson = JsonUtility.ToJson(localizationData);
        File.WriteAllText(filePath,dataAsJson);

        //Reload updated file
        LoadData();
    }

    private void RemoveData(Dialogue dialogue) {

        LocalizationItem localizationItem = BuildLocalizationItem(dialogue);

        LocalizationItem fileItem = localizationData.items.Find(data => data.key == localizationItem.key);;

        if (fileItem != null) {
            localizationData.items.Remove(fileItem);
        }

        string dataAsJson = JsonUtility.ToJson(localizationData);
        File.WriteAllText(filePath,dataAsJson);

        //Reload updated file
        LoadData();
    }

    private LocalizationItem BuildLocalizationItem(Dialogue dialogue) {

        List<string> valueList = new List<string>();
        int sentenceIndex = 0;
        int choiceIndex = 0;

        foreach (int element in dialogue.elementsOrder) {
            switch (dialogue.elementTypes[element]) {

                case "Sentence":
                    valueList.Add(dialogue.sentences[sentenceIndex].text);
                    sentenceIndex++;
                    break;

                case "Choice":
                    foreach (string option in dialogue.choices[choiceIndex].options) {
                        valueList.Add(option);
                    }
                    break;

            }
        }

        return new LocalizationItem(dialogue.key,valueList.ToArray());
    }
}