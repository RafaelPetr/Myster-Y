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

        if (string.IsNullOrEmpty(dialogue.key)) {
            key = EditorGUILayout.TextField("Key:",key);
            if (GUILayout.Button("Save Key")) {
                dialogue.key = key;
            }
        }
        else {
            EditorGUILayout.LabelField("Key: " + dialogue.key);
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
        for (int i = 0; i < dialogue.sentences.Count; i++) {
            if (dialogue.sentences[i].character != null) {
                GUILayout.Label("Text");
                dialogue.sentences[i].text = GUILayout.TextArea(dialogue.sentences[i].text);
            }
            GUILayout.BeginHorizontal();
                dialogue.sentences[i].character = (DialogueCharacter)EditorGUILayout.ObjectField("",dialogue.sentences[i].character,typeof(DialogueCharacter),true);
                if (GUILayout.Button("Delete")) {
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

    private void ChoiceTab(Dialogue dialogue) {
        if (dialogue.choice.GetEnable()) {
            GUILayout.BeginHorizontal();
                GUILayout.Label("Conext:");
                dialogue.choice.context = GUILayout.TextArea(dialogue.choice.context,GUILayout.Width(170));
            GUILayout.EndHorizontal();
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            
            for (int i = 0; i < dialogue.choice.options.Length; i++) {
                GUILayout.Label("Option " + (i+1).ToString());

                GUILayout.BeginHorizontal();
                    GUILayout.Label("Text:");
                    dialogue.choice.options[i].text = GUILayout.TextArea(dialogue.choice.options[i].text,GUILayout.Width(170));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Label("Dialogue:");
                    dialogue.choice.options[i].linkedDialogue = (Dialogue)EditorGUILayout.ObjectField("",dialogue.choice.options[i].linkedDialogue,typeof(Dialogue),true); 
                GUILayout.EndHorizontal();

                EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            }

            if (GUILayout.Button("Remove Choice")) {
                dialogue.RemoveChoice();
            }
            
        }
        else {
            if (GUILayout.Button("Add Choice")) {
                dialogue.AddChoice();
            }
        }
    }

    private void BuildTab(Dialogue dialogue) {
        for (int i = 0; i < dialogue.elementsOrder.Count; i++) {
            dialogue.elementsOrder[i] = 0; //While there is no other dialogue element for ordering
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
        Queue<DialogueSentence> sentenceQueue = new Queue<DialogueSentence>(dialogue.sentences);

        int localizationGroupIndex = 0;

        foreach (int element in dialogue.elementsOrder) {
            switch (dialogue.elementTypes[element]) {
                case "Sentence":
                    DialogueSentence sentence = sentenceQueue.Dequeue();
                    sentence.localizationGroupIndex = localizationGroupIndex;
                    valueList.Add(sentence.text);
                    break;
            }
            localizationGroupIndex++;
        }

        if (dialogue.choice.GetEnable()) {
            dialogue.choice.contextLocalizationGroupIndex = localizationGroupIndex;
            valueList.Add(dialogue.choice.context);
            localizationGroupIndex++;

            foreach (DialogueOption option in dialogue.choice.options) {
                option.localizationGroupIndex = localizationGroupIndex;
                valueList.Add(option.text);
                localizationGroupIndex++;
            }
        }
        return new LocalizationElement(dialogue.key,valueList.ToArray());
    }
}