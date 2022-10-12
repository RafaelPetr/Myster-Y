using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance;
    private EventSystem eventSystem;

    private Queue<DialogueElement> elements = new Queue<DialogueElement>();

    private Dialogueable activeDialogueable;
    private bool inDialogue;
    private DialogueChoice activeChoice;
    private int optionIndex;

    [SerializeField]private GameObject textBox;
    [SerializeField]private GameObject dialogueTextBox;
    private Animator textBoxAnimator;

    [SerializeField]private GameObject sentenceUI;
    [SerializeField]private TextMeshProUGUI sentenceName;
    [SerializeField]private TextMeshProUGUI sentenceText;
    [SerializeField]private Image sentenceIcon;

    [SerializeField]private GameObject choiceUI;
    [SerializeField]private TextMeshProUGUI choiceContext;
    [SerializeField]private GameObject choiceOptions;
    private List<Button> choiceOptionsButtons = new List<Button>();
    private List<TextMeshProUGUI> choiceOptionsTexts = new List<TextMeshProUGUI>();

    private string activeWritingText;
    private TextMeshProUGUI activeWritingUI;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        choiceUI.SetActive(true);
        choiceOptionsButtons = new List<Button>(choiceOptions.GetComponentsInChildren<Button>());
        choiceOptionsTexts = new List<TextMeshProUGUI>(choiceOptions.GetComponentsInChildren<TextMeshProUGUI>());
        choiceUI.SetActive(false);

        eventSystem = FindObjectOfType<EventSystem>();

        textBoxAnimator = textBox.GetComponent<Animator>();
    }

    public void SetOptionIndex(int index) {
        optionIndex = index;
    }

    public void ReceiveInteract(Dialogueable dialogueable) {
        if (activeWritingText != null) {
		    activeWritingUI.text = activeWritingText;
            FinishWrite();
            return;
        }
        else if (activeChoice != null) {
            SelectOption();
            return;
        }
        else if (inDialogue) {
            ExecuteNextElement();
        }       
        else {
            activeDialogueable = dialogueable;
            StartDialogue(dialogueable.DefineDialogue());
        }
    }

    private void ResetUI() {
        sentenceUI.SetActive(false);
        choiceUI.SetActive(false);
        textBoxAnimator.SetBool("Active",true);
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueTextBox.SetActive(true);

        inDialogue = true;
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
        sentenceName.text = sentence.character.name;
        sentenceIcon.sprite = sentence.character.icon;

        StartWriting(sentence.text, sentenceText);
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

        activeWritingText = text;
        activeWritingUI = writingUI;
		activeWritingUI.text = "";
		StartCoroutine(Write());
	}

	private IEnumerator Write() {
		foreach (char letter in activeWritingText.ToCharArray()) {
			activeWritingUI.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
        FinishWrite();
	}

	private void FinishWrite() {
		StopAllCoroutines();

		activeWritingText = null;
		activeWritingUI = null;
	}

    private void SelectOption() {
        choiceUI.SetActive(false);

        activeDialogueable.ExecuteFunction(activeChoice.options[optionIndex].function);

        if (activeChoice.options[optionIndex].linkedDialogue != null) {
            StartDialogue(activeChoice.options[optionIndex].linkedDialogue);
            return;
        }

        activeChoice = null;
        ExecuteNextElement();
    }

    public void EndDialogue() {
        dialogueTextBox.SetActive(false);
        inDialogue = false;
        textBoxAnimator.SetBool("Active",false);
        activeDialogueable.FinishInteraction();
    }

}
