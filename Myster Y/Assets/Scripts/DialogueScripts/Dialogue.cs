using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum elementTypes {Sentence,Choice};

[CreateAssetMenu(menuName = "Dialogue Objects/Dialogue")]
public class Dialogue : ScriptableObject {
	public string key;
	int localizationIndex = -1;
	
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

	public void GetLocalizedText() {
		int sentenceIndex = 0;
		int choiceIndex = 0;
		localizationIndex = -1;

		for (int i = 0; i < elementsOrder.Count; i++) {
			localizationIndex++;

			switch (elementTypes[elementsOrder[i]]) {
				case "Sentence":
					GetLocalizedSentence(sentenceIndex);
					sentenceIndex++;
					break;
				case "Choice":
					GetLocalizedChoice(choiceIndex);
					choiceIndex++;
					break;
			}
		}
	}

	private void GetLocalizedSentence(int sentenceIndex) {
		sentences[sentenceIndex].text = LocalizationManager.instance.GetLocalizedValue(key,localizationIndex);
	}

	private void GetLocalizedChoice(int choiceIndex) {
		string localizedText = null;
		List<string> localizedOptions = new List<string>();
		for (int i = 0; i < choices[choiceIndex].options.Count; i++) {
			localizedText = LocalizationManager.instance.GetLocalizedValue(key,localizationIndex);
			choices[choiceIndex].options[i].text = localizedText;
			
			localizationIndex++;
		}
	}
}
