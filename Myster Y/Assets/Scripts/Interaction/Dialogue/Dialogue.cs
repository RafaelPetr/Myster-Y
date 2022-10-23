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

    public void LocalizeElements() {
        foreach (DialogueSentence sentence in sentences) {
            sentence.LocalizeText(key);
        }

        if (choice.GetEnable()) {
            choice.LocalizeText(key);
        }
    }

    #region Add

        public void AddSentence() {
            sentences.Add(new DialogueSentence());
            elementsOrder.Add(-1);
        }

        public void AddChoice() {
            choice.SetEnable(true);
        }

        public void AddOption() {
            choice.AddOption();
        }

    #endregion

    #region Remove

        public void RemoveSentence(int index) {
            sentences.RemoveAt(index);
            elementsOrder.RemoveAt(index);
        }

        public void RemoveChoice() {
            choice.SetEnable(false);
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

        public DialogueSentence GetSentence(int index) {
            return sentences[index];
        }

        public DialogueChoice GetChoice() {
            return choice;
        }

        public List<int> GetOrder() {
            return elementsOrder;
        }

        public int GetOrderElement(int index) {
            return elementsOrder[index];
        }

        public string[] GetTypes() {
            return elementTypes;
        }

        public string GetType(int index) {
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
