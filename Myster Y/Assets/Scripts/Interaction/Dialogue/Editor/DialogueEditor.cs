using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor {
    private int tabIndex;
    private string filePath;
    private string key;

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
            DialogueSentence sentence = dialogue.GetSentence(i);

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

        if (choice.GetEnable()) {
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
                DialogueOption option = choice.GetOption(i);

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
            dialogue.SetOrderElement(i, EditorGUILayout.Popup(dialogue.GetOrderElement(i), dialogue.GetTypes()));
        }

        EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            if (GUILayout.Button("Load Data")) {
                filePath = null;
                LoadData();
            }

        GUILayout.BeginHorizontal();

            //if (localizationData != null) {
                if (GUILayout.Button("Save Data")) {
                    SaveData(dialogue);
                }
                if (GUILayout.Button("Remove Data")) {
                    RemoveData(dialogue);
                }
            //}

        GUILayout.EndHorizontal();
    }

    private void LoadData() {
        filePath = Application.streamingAssetsPath + "/Localization/json_localization_ptbr.json";

        if (string.IsNullOrEmpty(filePath)) {
            filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
        }

        string dataAsJson = File.ReadAllText(filePath);
        localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
    }

    private void SaveData(Dialogue dialogue) {
        LoadData();

        LocalizationElement localizationElement = BuildLocalizationElement(dialogue);
        LocalizationGroup fileGroup = localizationData.groups.Find(group => group.key == "dialogues");

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
            List<LocalizationElement> elementsList = new List<LocalizationElement>();
            elementsList.Add(localizationElement);
            fileGroup = new LocalizationGroup("dialogues", elementsList);

            localizationData.groups.Add(fileGroup);
        }

        string dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(filePath,dataAsJson);

        LoadData();
    }

    private void RemoveData(Dialogue dialogue) {
        LoadData();

        LocalizationElement localizationItem = BuildLocalizationElement(dialogue);
        LocalizationGroup fileGroup = localizationData.groups.Find(group => group.key == "dialogues");
        LocalizationElement fileItem = fileGroup.elements.Find(data => data.key == localizationItem.key);;

        if (fileItem != null) {
            fileGroup.elements.Remove(fileItem);

            if (fileGroup.elements.Count == 0) {
                localizationData.groups.Remove(fileGroup);
            }
        }

        string dataAsJson = JsonUtility.ToJson(localizationData,true);
        File.WriteAllText(filePath,dataAsJson);

        LoadData();
    }

    private LocalizationElement BuildLocalizationElement(Dialogue dialogue) {
        List<string> valueList = new List<string>();
        Queue<DialogueSentence> sentenceQueue = new Queue<DialogueSentence>(dialogue.GetSentences());

        int locIndex = 0;

        foreach (int element in dialogue.GetOrder()) {
            switch (dialogue.GetType(element)) {
                case "Sentence":
                    DialogueSentence sentence = sentenceQueue.Dequeue();

                    sentence.SetLocIndex(locIndex);
                    valueList.Add(sentence.GetText());
                    break;
            }
            locIndex++;
        }

        DialogueChoice choice = dialogue.GetChoice();

        if (choice.GetEnable()) {
            choice.SetLocIndex(locIndex);
            valueList.Add(choice.GetContext());

            locIndex++;

            foreach (DialogueOption option in choice.GetOptions()) {
                option.SetLocIndex(locIndex);
                valueList.Add(option.GetText());

                locIndex++;
            }
        }

        return new LocalizationElement(dialogue.GetKey(),valueList.ToArray());
    }
}