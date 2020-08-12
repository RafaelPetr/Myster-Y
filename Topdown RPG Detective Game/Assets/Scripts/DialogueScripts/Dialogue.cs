using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueSceneScript{DialogueSentence,DialogueChoice}

[System.Serializable]
public class Dialogue {
	public DialogueSceneScript[] dialogueSceneScript;
	public List<DialogueSentence> dialogueSentences;
	public List<DialogueChoice> dialogueChoices;

}