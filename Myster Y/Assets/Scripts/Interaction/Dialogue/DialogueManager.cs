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

    private bool executingDialogue;
    private string activeText;
    private TextMeshProUGUI activeWritingUI;
    private DialogueChoice activeChoice;

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

    public bool GetIsChoosing() {
        return activeChoice != null;
    }

    public void ReceiveInteract(Dialogue dialogue, int optionIndex = -1) {
        if (activeText != null) {
            FinishWrite();
            return;
        }
        else if (activeChoice != null) {
            SelectOption(optionIndex);
            return;
        }
        else if (executingDialogue) {
            ExecuteNextElement();
        }       
        else {
            StartDialogue(dialogue);
            PlayerController.instance.SetInInteraction(true);
        }
    }

    private void ResetUI() {
        sentenceUI.SetActive(false);
        choiceUI.SetActive(false);
        dialogueBoxAnimator.SetBool("Active",true);
    }

    public void StartDialogue(Dialogue dialogue) {
        executingDialogue = true;
        activeChoice = null;

        foreach (DialogueSentence sentence in dialogue.sentences) {
            elements.Enqueue(sentence);
        }
        if (dialogue.choice.GetEnable()) {
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
        activeChoice = choice;
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

    private void SelectOption(int optionIndex) {
        choiceUI.SetActive(false);

        if (activeChoice.options[optionIndex].linkedDialogue != null) {
            StartDialogue(activeChoice.options[optionIndex].linkedDialogue);
            return;
        }

        activeChoice = null;
        ExecuteNextElement();
    }

    public void EndDialogue() {
        executingDialogue = false;
        dialogueBoxAnimator.SetBool("Active",false);
        PlayerController.instance.SetInInteraction(false);
    }

}
