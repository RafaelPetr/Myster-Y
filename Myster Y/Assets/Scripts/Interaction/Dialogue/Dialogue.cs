using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum elementTypes {Sentence,Choice};

[CreateAssetMenu(fileName = "scriptable_dialogue_", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject {
    private string key;

    private List<DialogueSentence> sentences = new List<DialogueSentence>();
    private DialogueChoice choice;

    private List<int> elementsOrder = new List<int>();
    private string[] elementTypes = new string[]{"Sentence"};

    public void LoadData(DialogueData dialogue) {
        sentences.Clear();
        choice = null;

        for (int i = 0; i < sentences.Count; i++) {
            SentenceData sentenceData = dialogue.GetSentences(i);

            //sentences[i].SetCharacter(sentenceData.GetCharacter()); Carregar arquivo de pasta resources
            sentences[i].SetText(sentenceData.GetText());
        }

        ChoiceData choiceData = dialogue.GetChoice();
        if (choiceData != null) {
            choice.SetContext(choiceData.GetContext());

            for (int i = 0; i < choiceData.GetOptions().Count; i++) {
                OptionData optionData = choiceData.GetOptions(i);

                
            }
        }
    }

    public void Localize() {
        LocalizationDialogue dialogue = LocalizationManager.GetLocalizedDialogue(key);

        for (int i = 0; i < sentences.Count; i++) {
            LocalizationSentence localizedSentence = dialogue.GetSentences(i);
            sentences[i].SetText(localizedSentence.GetText());
        }

        if (choice != null) {
            LocalizationChoice localizedChoice = dialogue.GetChoice();
            choice.SetContext(localizedChoice.GetContext());

            for (int i = 0; i < choice.GetOptions().Count; i++) {
                DialogueOption option = choice.GetOptions(i);
                LocalizationOption localizedOption = localizedChoice.GetOptions(i);

                option.SetText(localizedOption.GetText());
            }
        }
    }

    #region Add

        public void AddSentence() {
            sentences.Add(new DialogueSentence());
            elementsOrder.Add(-1);
        }

        public void AddSentence(string value) {
            DialogueSentence sentence = new DialogueSentence();
            sentence.SetText(value);

            sentences.Add(sentence);
            elementsOrder.Add(-1);
        }

        public void AddChoice() {
            choice = new DialogueChoice();
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
            elementsOrder.RemoveAt(index);
        }

        public void RemoveChoice() {
            choice = null;
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
            return elementsOrder;
        }

        public int GetOrder(int index) {
            return elementsOrder[index];
        }

        public string[] GetTypes() {
            return elementTypes;
        }

        public string GetTypes(int index) {
            return elementTypes[index];
        }

    #endregion

    #region Setters

        public void SetKey(string value) {
            key = value;
        }

        public void SetOrderElement(int index, int value) {
            elementsOrder[index] = value;
        }

    #endregion
}
