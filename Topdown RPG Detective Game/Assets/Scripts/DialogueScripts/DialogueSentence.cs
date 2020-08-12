using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueSentence : DialogueElement {
    public string name;
	[TextArea(3, 10)]
    public string text;

    public override void ExecuteElement() {
        defineManager();

        dialogueManager.choiceBox.gameObject.SetActive(false);
        dialogueManager.sentenceBox.gameObject.SetActive(true);

        dialogueManager.nameText.text = name;
		dialogueManager.iconBox.sprite = icon;
        dialogueManager.StartWriting(text,dialogueManager.sentenceText);
    }

    public override void CompleteWrite() {
        dialogueManager.FinishWrite(text,dialogueManager.sentenceText);
    }

}
