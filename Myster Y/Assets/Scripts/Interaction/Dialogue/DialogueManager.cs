using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance;

    private Queue<DialogueElement> elements = new Queue<DialogueElement>();

    public GameObject dialogueBox;
    private Animator dialogueBoxAnimator;

    private EventSystem eventSystem;

    public GameObject sentenceUI;
    public TextMeshProUGUI sentenceName;
    public TextMeshProUGUI sentenceText;
    public Image sentenceIcon;

    public GameObject choiceUI;
    public TextMeshProUGUI choiceContext;
    public GameObject choiceOptions;
    private List<Button> choiceOptionsButtons = new List<Button>();
    private List<TextMeshProUGUI> choiceOptionsTexts = new List<TextMeshProUGUI>();

    private string activeText;
    private TextMeshProUGUI activeWritingUI;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        choiceUI.SetActive(true);
        choiceOptionsButtons = new List<Button>(choiceOptions.GetComponentsInChildren<Button>());
        choiceOptionsTexts = new List<TextMeshProUGUI>(choiceOptions.GetComponentsInChildren<TextMeshProUGUI>());
        choiceUI.SetActive(false);

        eventSystem = FindObjectOfType<EventSystem>();

        dialogueBoxAnimator = dialogueBox.GetComponent<Animator>();
    }

    public bool GetIsWriting() {
        return activeText != null;
    }

    public bool GetInChoice() {
        return activeText != null;
    }

    private void ResetUI() {
        sentenceUI.SetActive(false);
        choiceUI.SetActive(false);
        dialogueBoxAnimator.SetBool("Active",true);
    }

    public void StartDialogue(Dialogue dialogue) {
        foreach (DialogueSentence sentence in dialogue.sentences) {
            elements.Enqueue(sentence);
        }
        if (dialogue.choice.enabled) {
            elements.Enqueue(dialogue.choice);
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

    public void UpdateSentenceUI(DialogueSentence sentence) {
        ResetUI();
        sentenceUI.SetActive(true);
        sentenceName.text = sentence.character.characterName;
        sentenceIcon.sprite = sentence.character.icon;

        StartWriting(sentence.text,sentenceText);
    }

    public void UpdateChoiceUI(DialogueChoice choice) {
        ResetUI();
        choiceUI.SetActive(true);

 		eventSystem.SetSelectedGameObject(null);

        StartWriting(choice.context, choiceContext);
        
        for (int i = 0; i < choiceOptionsButtons.Count; i++) {
            if (choice.options[i].text != "") {
                choiceOptionsTexts[i].text = choice.options[i].text;
            }
            else {
                choiceOptionsButtons[i].gameObject.SetActive(false);
            }
        }
        
        choiceOptionsButtons[0].Select();
    }

    private void StartWriting(string text, TextMeshProUGUI writingUI) {
		StopAllCoroutines();

        activeText = text;
        activeWritingUI = writingUI;
		activeWritingUI.text = "";
		StartCoroutine(Write());
	}

	IEnumerator Write() {
		foreach (char letter in activeText.ToCharArray()) {
			activeWritingUI.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
		activeText = null;
		activeWritingUI = null;
	}

	public void FinishWrite() {
		StopAllCoroutines();
		activeWritingUI.text = activeText;

		activeText = null;
		activeWritingUI = null;
	}

    public void EndDialogue() {
        dialogueBoxAnimator.SetBool("Active",false);
        PlayerController.instance.SetInInteraction(false);
    }

}
