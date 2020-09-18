using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager instance;

	public Animator animator;
    public Text nameText;
	public Text sentenceText;
	public Button[] optionButtons = new Button[3];
	public Image iconBox;

	[System.NonSerialized]public bool inDialogue;
	[System.NonSerialized]public bool inChoice;

	private string activeText;
	
	private List<DialogueSentence> sentences;
	private List<DialogueChoice> choices;
	private Queue<DialogueElement> elements;

	private DialogueSource activeSource;

	void Start() {

		if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

		elements = new Queue<DialogueElement>();
	}

	public void ReceiveInteract(DialogueSource dialogueSource) {
		activeSource = dialogueSource;

		if (activeText != null) {
			FinishWrite(activeText);
		}

		else {
			if (!inDialogue) {
				Dialogue dialogue = activeSource.BuildDialogue();
				StartDialogue(dialogue);
			}
			else {
				ExecuteNextElement();
			}
		}
	}

	public void StartDialogue(Dialogue dialogue) {
		animator.SetBool("IsOpen", true);

		inDialogue = true;
		activeSource.SetInDialogue(true);

		sentences = dialogue.sentences;
		choices = dialogue.choices;

		elements.Clear();

		int sentenceIndex = 0;
		int choiceIndex = 0;

		foreach (int element in dialogue.elementsOrder) {
			switch(dialogue.elementTypes[element]) {
				case "Sentence":
					elements.Enqueue(sentences[sentenceIndex]);
					sentenceIndex++;
					break;
				case "Choice":
					elements.Enqueue(choices[choiceIndex]);
					choiceIndex++;
					break;
			}
		}
		ExecuteNextElement();
	}

	public void ExecuteNextElement() {
		if (elements.Count == 0) {
			EndDialogue();
			return;
		}
		DialogueElement element = elements.Dequeue();
		element.Execute();
	}

	public void StartWriting(string text) {
		activeText = text;
		StopAllCoroutines();
		StartCoroutine(Write(text));
	}

	IEnumerator Write(string text) {
		sentenceText.text = "";
		foreach (char letter in text.ToCharArray()) {
			sentenceText.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
		activeText = null;
	}

	public void FinishWrite(string text) {
		StopAllCoroutines();
		sentenceText.text = text;
		activeText = null;
	}

	public void PickOption(int choiceIndex) {

	}

	public void UpdateDialogueBox(DialogueElement element) {
		for (int i = 0; i < optionButtons.Length; i++) {
			optionButtons[i].gameObject.active = false;
		}

		nameText.text = element.character.name;
		iconBox.sprite = element.character.icon;
	}

	public void DefineChoiceButtons(List<DialogueOption> options) {

		for (int i = 0; i < options.Count; i++) {
			optionButtons[i].gameObject.active = true;
			optionButtons[i].GetComponentInChildren<Text>().text = options[i].text;
		}

	}

	void EndDialogue() {
		animator.SetBool("IsOpen", false);
		inDialogue = false;
		activeSource.SetInDialogue(false);
	}
}