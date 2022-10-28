using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor {
    private int tabIndex;
    private string key;

    private string dataPath;
    private string localizationPath;

    private DataDialogueList dataDialogueList;
    private LocalizationData localizationData;

    public override void OnInspectorGUI() {
        Dialogue dialogue = (Dialogue)target;

        if (string.IsNullOrEmpty(key)) {
            key = dialogue.name;
        }

        if (string.IsNullOrEmpty(dialogue.GetKey())) {
            key = EditorGUILayout.TextField("Key:",key);

            if (GUILayout.Button("Save Key")) {
                dialogue.SetKey(key);
            }
        }
        else {
            EditorGUILayout.LabelField("Key: " + dialogue.GetKey());
            tabIndex = GUILayout.Toolbar(tabIndex, new string[]{"Sentences","Choice","Build"});

            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            switch(tabIndex) {
                case 0:
                    SentencesTab(dialogue);
                    break;
                case 1:
                    ChoiceTab(dialogue);
                    break;
                case 2:
                    BuildTab(dialogue);
                    break;
            }
        }
        
        EditorUtility.SetDirty(dialogue);
    } 

    private void SentencesTab(Dialogue dialogue) {
        for (int i = 0; i < dialogue.GetSentences().Count; i++) {
            DialogueSentence sentence = dialogue.GetSentences(i);

            GUILayout.Label("Text");
            sentence.SetText(GUILayout.TextArea(sentence.GetText()));
            GUILayout.BeginHorizontal();
                sentence.SetCharacter((DialogueCharacter)EditorGUILayout.ObjectField("",sentence.GetCharacter(),typeof(DialogueCharacter),true));
                if (GUILayout.Button("Delete")) {
                    dialogue.RemoveSentence(i);
                }
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
        }

        GUILayout.Label("Sentences: " + dialogue.GetSentences().Count.ToString());

        if (GUILayout.Button("Add Sentence")) {
            dialogue.AddSentence();
        }
    }

    private void ChoiceTab(Dialogue dialogue) {
        DialogueChoice choice = dialogue.GetChoice();

        if (choice.GetEnabled()) {
            if (GUILayout.Button("Remove Choice")) {
                dialogue.RemoveChoice();
            }
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            GUILayout.BeginHorizontal();
                GUILayout.Label("Context:");
                choice.SetContext(GUILayout.TextArea(choice.GetContext(),GUILayout.Width(170)));
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            
            for (int i = 0; i < choice.GetOptions().Count; i++) {
                DialogueOption option = choice.GetOptions(i);

                GUILayout.Label("Option " + (i+1).ToString());

                GUILayout.BeginHorizontal();
                    GUILayout.Label("Text:");
                    option.SetText(GUILayout.TextArea(option.GetText(),GUILayout.Width(170)));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Label("Function:");
                    option.SetFunction(GUILayout.TextArea(option.GetFunction()));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Remove Option")) {
                    dialogue.RemoveOption(i);
                }

                EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            }

            if (GUILayout.Button("Add Option")) {
                dialogue.AddOption();
            }
        }
        else {
            if (GUILayout.Button("Add Choice")) {
                dialogue.AddChoice();
            }
        }
    }

    private void BuildTab(Dialogue dialogue) {
        for (int i = 0; i < dialogue.GetOrder().Count; i++) {
            dialogue.SetOrderElement(i, 0); //While there is no other dialogue element for ordering
            dialogue.SetOrderElement(i, EditorGUILayout.Popup(dialogue.GetOrder(i), dialogue.GetTypes()));
        }

        EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            if (GUILayout.Button("Load Data")) {
                LoadData(dialogue);
            }

        GUILayout.BeginHorizontal();

            if (GUILayout.Button("Save Data")) {
                SaveData(dialogue);
            }
            if (GUILayout.Button("Remove Data")) {
                RemoveData(dialogue);
            }

        GUILayout.EndHorizontal();
    }

    private void LoadData() {
        /*if (string.IsNullOrEmpty(filePath)) {
            filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
        }*/

        localizationPath = Application.streamingAssetsPath + "/Localization/json_localization_ptbr.json";
        string dataAsJson = File.ReadAllText(localizationPath);
        localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        dataPath = Application.streamingAssetsPath + "/Dialogue/json_dialogueData.json";
        string dialogueDataAsJson = File.ReadAllText(dataPath);
        dataDialogueList = JsonUtility.FromJson<DataDialogueList>(dialogueDataAsJson);
    }

    private void LoadData(Dialogue dialogue) {
        LoadData();

        DataDialogue dataDialogue = dataDialogueList.GetDialogues().Find(data => data.GetKey() == dialogue.GetKey());

        if (dataDialogue != null) {
            dialogue.LoadData(dataDialogue);
        }
    }

    private void SaveData(Dialogue dialogue) {
        string dataAsJson;
        
        LoadData();

        RemoveData(dialogue);

        DataDialogue fileData = new DataDialogue(dialogue);
        dataDialogueList.AddDialogue(fileData);
        dataAsJson = JsonUtility.ToJson(dataDialogueList,true);
        File.WriteAllText(dataPath,dataAsJson);

        LocalizationDialogue fileLocalization = new LocalizationDialogue(dialogue);
        localizationData.AddDialogue(fileLocalization);
        dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(localizationPath,dataAsJson);

        LoadData();
    }

    private void RemoveData(Dialogue dialogue) {
        LoadData();

        DataDialogue fileData = dataDialogueList.GetDialogues().Find(data => data.GetKey() == dialogue.GetKey());
        LocalizationDialogue fileLocalization = localizationData.GetDialogues().Find(data => data.GetKey() == dialogue.GetKey());

        if (fileData != null) {
            dataDialogueList.RemoveDialogue(fileData);
        }

        if (fileLocalization != null) {
            localizationData.RemoveDialogue(fileLocalization);
        }

        string dataAsJson = JsonUtility.ToJson(dataDialogueList,true);
        File.WriteAllText(dataPath,dataAsJson);

        dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(localizationPath,dataAsJson);

        LoadData();
    }
}