using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Dialogue : ScriptableObject{
	public List<DialogueElement> dialogueElements = new List<DialogueElement>();

	public void NewSentence() {
		dialogueElements.Add(new DialogueSentence());
		Debug.Log(dialogueElements);
	}

	private void Awake() {
		Debug.Log(dialogueElements[0]);
	}

}