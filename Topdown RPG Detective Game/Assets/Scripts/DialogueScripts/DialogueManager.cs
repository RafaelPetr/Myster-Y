using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public Animator animator;
	public PlayerController controller;
	public GameObject sentenceBox;
	public GameObject choiceBox;
    public Text nameText;
	public Text sentenceText;
	public Text choiceContext;
	public Button choicePossibility1;
	public Button choicePossibility2;
	public Button choicePossibility3;
	public Image iconBox;

	[System.NonSerialized]public bool inDialogue;
	[System.NonSerialized]public bool inChoice;
    [System.NonSerialized]public Button selectedChoice;

	public DialogueElement activeWriteElement;
	public DialogueChoice activeChoice;
	private List<DialogueSentence> sentences;
	private List<DialogueChoice> choices;
	private Queue<DialogueElement> elements;

	private DialogueSource activeSource;

	void Start() {
		elements = new Queue<DialogueElement>();
	}

	public void ReceiveInteract(DialogueSource dialogueSource) {
		activeSource = dialogueSource;
		Dialogue dialogue = activeSource.StartDialogue();
		if (activeWriteElement != null) {
			activeWriteElement.CompleteWrite();
		}
		else if (!inChoice) {
            if (!inDialogue) {
                StartDialogue(dialogue);
            }
            else { 
                ExecuteNextElement();
            }
        }
        else {
            selectedChoice.onClick.Invoke();
        }
	}

	public void StartDialogue(Dialogue dialogue) {
		inDialogue = true;
		controller.inDialogue = true;

		animator.SetBool("IsOpen", true);

		sentences = dialogue.dialogueSentences;
		choices = dialogue.dialogueChoices;

		elements.Clear();

		int sentenceIndex = 0;
		int choiceIndex = 0;

		foreach (DialogueSceneScript element in dialogue.dialogueSceneScript)
		{
			switch(element) {
				case DialogueSceneScript.DialogueSentence:
					elements.Enqueue(sentences[sentenceIndex]);
					sentenceIndex++;
					break;
				case DialogueSceneScript.DialogueChoice:
					elements.Enqueue(choices[choiceIndex]);
					choiceIndex++;
					break;
			}
		}	
		ExecuteNextElement();
	}

	public void ExecuteNextElement() {
		if (elements.Count == 0)
		{
			EndDialogue();
			return;
		}
		DialogueElement element = elements.Dequeue();
		activeWriteElement = element;
		element.ExecuteElement();
	}

	public void StartWriting(string text,Text box) {
		StopAllCoroutines();
		StartCoroutine(Write(text,box));
	}


	IEnumerator Write(string text,Text box) {
		box.text = "";
		foreach (char letter in text.ToCharArray())
		{
			box.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
		activeWriteElement = null;
	}

	public void FinishWrite(string text,Text box) {
		StopAllCoroutines();
		box.text = text;
		activeWriteElement = null;
	}

	public void ChoosePossibility(int choiceNumber) {
		inChoice = false;
		selectedChoice = null;
		GameObject myEventSystem = GameObject.Find("EventSystem");
 		myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
		StartDialogue(activeChoice.optionDialogues[choiceNumber]);
	}

	void EndDialogue() {
		animator.SetBool("IsOpen", false);
		inDialogue = false;
		controller.inDialogue = false;
		activeSource.EndDialogue();
	}
}