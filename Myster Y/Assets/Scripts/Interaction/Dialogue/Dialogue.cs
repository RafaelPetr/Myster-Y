using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum elementTypes {Sentence,Choice};

[CreateAssetMenu(fileName = "scriptable_dialogue_", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject {
    [SerializeField]private string key;

    [SerializeField]private List<DialogueSentence> sentences = new List<DialogueSentence>();
    [SerializeField]private DialogueChoice choice;

    [SerializeField]private List<int> order = new List<int>();
    [SerializeField]private string[] types = new string[]{"Sentence"};

    public void LoadData(DataDialogue dialogue) {
        choice = null;
        sentences.Clear();

        for (int i = 0; i < dialogue.GetSentences().Count; i++) {
            AddSentence();
            DataSentence sentenceData = dialogue.GetSentences(i);

            if (sentenceData.GetCharacter() != string.Empty) {
                DialogueCharacter character = (DialogueCharacter)Resources.Load("Scriptables/Dialogues/Characters/" + sentenceData.GetCharacter());
                sentences[i].SetCharacter(character);
            }
            sentences[i].SetText(sentenceData.GetText());
        }

        DataChoice choiceData = dialogue.GetChoice();
        if (choiceData.GetOptions().Count > 0) {
            choice = new DialogueChoice();
            AddChoice();
            choice.SetContext(choiceData.GetContext());

            for (int i = 0; i < choiceData.GetOptions().Count; i++) {
                choice.AddOption();

                DialogueOption option = choice.GetOptions(i);
                DataOption optionData = choiceData.GetOptions(i);

                option.SetText(optionData.GetText());
                option.SetFunction(optionData.GetFunction());
            }
        }

        order = dialogue.GetOrder();
    }

    public void Localize() {
        LocalizationDialogue dialogue = LocalizationManager.GetLocalizedDialogue(key);

        if (dialogue != null) {
            for (int i = 0; i < sentences.Count; i++) {
                LocalizationSentence localizedSentence = dialogue.GetSentences(i);

                sentences[i].SetText(localizedSentence.GetText());
            }

            if (choice.GetEnabled()) {
                LocalizationChoice localizedChoice = dialogue.GetChoice();
                choice.SetContext(localizedChoice.GetContext());

                for (int i = 0; i < choice.GetOptions().Count; i++) {
                    DialogueOption option = choice.GetOptions(i);
                    LocalizationOption localizedOption = localizedChoice.GetOptions(i);

                    option.SetText(localizedOption.GetText());
                }
            }
        }
    }

    #region Add

        public void AddSentence() {
            sentences.Add(new DialogueSentence());
            order.Add(-1);
        }

        public void AddSentence(string value) {
            DialogueSentence sentence = new DialogueSentence();
            sentence.SetText(value);

            sentences.Add(sentence);
            order.Add(-1);
        }

        public void AddChoice() {
            choice.SetEnabled(true);
        }

        public void AddOption() {
            choice.AddOption();
        }

    #endregion

    #region Remove

        public void Reset() {
            sentences.Clear();
        }

        public void RemoveSentence(int index) {
            sentences.RemoveAt(index);
            order.RemoveAt(index);
        }

        public void RemoveChoice() {
            choice.SetEnabled(false);
        }

        public void RemoveOption(int index) {
            choice.RemoveOption(index);
        }

    #endregion

    #region Getters

        public string GetKey() {
            return key;
        }

        public List<DialogueSentence> GetSentences() {
            return sentences;
        }

        public DialogueSentence GetSentences(int index) {
            return sentences[index];
        }

        public DialogueChoice GetChoice() {
            return choice;
        }

        public List<int> GetOrder() {
            return order;
        }

        public int GetOrder(int index) {
            return order[index];
        }

        public string[] GetTypes() {
            return types;
        }

        public string GetTypes(int index) {
            return types[index];
        }

    #endregion

    #region Setters

        public void SetKey(string value) {
            key = value;
        }

        public void SetOrderElement(int index, int value) {
            order[index] = value;
        }

    #endregion
}
