using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueChoice : DialogueElement {

	[TextArea(3, 10)]
    public string contextText;
    [TextArea(3, 10)]
    public string[] possibilities = new string[3];
    public Dialogue[] optionDialogues = new Dialogue[3];

    public override void ExecuteElement() {
        defineManager();

        dialogueManager.activeChoice = this;

        dialogueManager.sentenceBox.gameObject.SetActive(false);
        dialogueManager.choiceBox.gameObject.SetActive(true);

        dialogueManager.choicePossibility1.GetComponentInChildren<Text>().text = possibilities[0];
		dialogueManager.choicePossibility2.GetComponentInChildren<Text>().text = possibilities[1];
		dialogueManager.choicePossibility3.GetComponentInChildren<Text>().text = possibilities[2];

		dialogueManager.choicePossibility1.Select();

        dialogueManager.inChoice = true;
        dialogueManager.StartWriting(contextText,dialogueManager.choiceContext);
    }

    public override void CompleteWrite() {
        dialogueManager.FinishWrite(contextText,dialogueManager.choiceContext);
    }

}
