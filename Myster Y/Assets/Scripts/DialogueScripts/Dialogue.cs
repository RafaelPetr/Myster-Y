using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum elementTypes {Sentence,Choice};

[CreateAssetMenu(menuName = "Dialogue Objects/Dialogue")]
public class Dialogue : ScriptableObject {
	public string key;
	
	public List<DialogueSentence> sentences = new List<DialogueSentence>();
	public List<DialogueChoice> choices = new List<DialogueChoice>();
	public List<int> elementsOrder = new List<int>();

    public string[] elementTypes = new string[]{"Sentence","Choice"};

	public void AddSentence() {
		sentences.Add(new DialogueSentence());
		elementsOrder.Add(-1);
	}

	public void AddChoice() {
		choices.Add(new DialogueChoice());
		elementsOrder.Add(-1);
	}

	public void RemoveSentence(int index) {
		if (sentences.Count > 0) {
			sentences.RemoveAt(index);
			elementsOrder.RemoveAt(index);
		}
	}

	public void RemoveChoice(int index) {
		if (choices.Count > 0) {
			choices.RemoveAt(index);
			elementsOrder.RemoveAt(index);
		}
	}
}
