using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance;

    private Queue<DialogueElement> elements = new Queue<DialogueElement>();

    public GameObject dialogueBox;

    public GameObject sentenceUI;
    public TextMeshProUGUI sentenceName;
    public TextMeshProUGUI sentenceText;

    public GameObject choiceUI;
    public TextMeshProUGUI choiceContext;
    public Button[] choiceOptions = new Button[3];

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        foreach (DialogueSentence sentence in dialogue.sentences) {
            elements.Enqueue(sentence);
        }
        if (dialogue.choice.enabled) {
            elements.Enqueue(dialogue.choice);
        }
        sentenceName.text = "lel";

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

    public void EndDialogue() {
        PlayerController.instance.SetInInteraction(false);
    }

}
