using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum elementTypes {Sentence,Choice};

[CreateAssetMenu(fileName = "scriptable_dialogue_", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject {
    public string key;

    public List<DialogueSentence> sentences = new List<DialogueSentence>();
    public DialogueChoice choice;

    public List<int> elementsOrder = new List<int>();
    [System.NonSerialized]public string[] elementTypes = new string[]{"Sentence"};

    public void AddSentence() {
        sentences.Add(new DialogueSentence());
		elementsOrder.Add(-1);
    }

    public void RemoveSentence(int index) {
        sentences.RemoveAt(index);
        elementsOrder.RemoveAt(index);
    }

    public void AddChoice() {
        choice.SetEnable(true);
    }

    public void RemoveChoice() {
        choice.SetEnable(false);
    }

    public void AddOption() {
        choice.options.Add(new DialogueOption());
    }

    public void RemoveOption(int index) {
        choice.options.RemoveAt(index);
    }

    public void LocalizeElements() {
        foreach (DialogueSentence sentence in sentences) {
            sentence.LocalizeText(key);
        }

        if (choice.GetEnable()) {
            choice.LocalizeText(key);
        }
    }
}
