using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager instance;

	[System.NonSerialized]public string localizationKey;
	[System.NonSerialized]public int localizationIndex = -1;

	public Animator animator;
    public Text nameText;
	public Text sentenceText;
	public Button[] optionButtons = new Button[3];
	public Image iconBox;

	[System.NonSerialized]public bool inDialogue;
	[System.NonSerialized]public bool inChoice;

	public DialogueElement activeElement;
	
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

		if (activeElement != null) {
			activeElement.Complete();
		}

		else {
			if (!inDialogue) {
				Dialogue dialogue = activeSource.DefineDialogue();
				localizationKey = dialogue.key;
				StartDialogue(dialogue);
			}
			else {
				ExecuteNextElement();
			}
		}
	}

	public void StartDialogue(Dialogue dialogue) {
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
		Debug.Log(elements.Count);
		if (elements.Count == 0) {
			EndDialogue();
			return;
		}
		activeElement = elements.Dequeue();
		activeElement.Execute();
	}

	public void StartWriting(string text) {
		StopAllCoroutines();
		StartCoroutine(Write(text));
	}

	IEnumerator Write(string text) {
		sentenceText.text = "";
		foreach (char letter in text.ToCharArray()) {
			sentenceText.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
		activeElement = null;
	}

	public void FinishWrite(string text) {
		StopAllCoroutines();
		sentenceText.text = text;
		activeElement = null;
	}

	public void InitDialogueBox(DialogueElement element) {
		animator.SetBool("IsOpen", true);

		nameText.text = element.character.name;
		iconBox.sprite = element.character.icon;
	}

	public void DefineChoiceButtons(List<string> options) {

		for (int i = 0; i < options.Count; i++) {
			optionButtons[i].GetComponentInChildren<Text>().text = options[i];
		}

	}

	void EndDialogue() {
		animator.SetBool("IsOpen", false);
		inDialogue = false;
		localizationKey = null;
		localizationIndex = -1;
		activeSource.SetInDialogue(false);
	}
}