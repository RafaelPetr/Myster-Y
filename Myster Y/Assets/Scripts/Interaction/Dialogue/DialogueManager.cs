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
    [SerializeField]private GameObject optionPrefab;
    private List<Button> choiceOptionButtons = new List<Button>();

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
        choiceUI.SetActive(false);

        eventSystem = FindObjectOfType<EventSystem>();

        textBoxAnimator = textBox.GetComponent<Animator>();
    }

    public void SetOptionIndex(int index) {
        optionIndex = index;
    }

    public void Interact(Dialogueable dialogueable) {
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

        foreach (Transform child in choiceOptions.transform) {
            Destroy(child.gameObject);
        }
        choiceOptionButtons.Clear();

        textBoxAnimator.SetBool("Active",true);
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueTextBox.SetActive(true);

        inDialogue = true;
        activeChoice = null;

        foreach (DialogueSentence sentence in dialogue.GetSentences()) {
            elements.Enqueue(sentence);
        }
        if (dialogue.GetChoice() != null) {
            elements.Enqueue(dialogue.GetChoice());
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
        sentenceName.gameObject.SetActive(false);
        sentenceIcon.gameObject.SetActive(false);

        DialogueCharacter character = sentence.GetCharacter();

        if (character != null) {
            sentenceName.gameObject.SetActive(true);
            sentenceIcon.gameObject.SetActive(true);
            
            sentenceName.text = character.GetName();
            sentenceIcon.sprite = character.GetIcon();
        }

        StartWriting(sentence.GetText(), sentenceText);
    }

    public void UpdateChoiceUI(DialogueChoice choice) {
        ResetUI();
        activeChoice = choice;
        choiceUI.SetActive(true);

 		eventSystem.SetSelectedGameObject(null);

        StartWriting(choice.GetContext(), choiceContext);
        
        for (int i = 0; i < choice.GetOptions().Count; i++) {
            
            DialogueOption option = choice.GetOptions(i);

            if (option.GetText() != "") {
                GameObject optionButton = Instantiate(optionPrefab);
                optionButton.transform.SetParent(choiceOptions.transform);

                optionButton.GetComponentInChildren<TextMeshProUGUI>().text = option.GetText();
                optionButton.GetComponent<DialogueOptionButton>().index = i;
                choiceOptionButtons.Add(optionButton.GetComponent<Button>());
                int lastIndex = choiceOptionButtons.Count - 1;

                if (lastIndex > 0) {
                    Navigation lastNavigation = choiceOptionButtons[lastIndex].navigation;
                    lastNavigation.mode = Navigation.Mode.Explicit;
                    lastNavigation.selectOnUp = choiceOptionButtons[lastIndex-1];
                    choiceOptionButtons[lastIndex].navigation = lastNavigation;

                    Navigation previousNavigation = choiceOptionButtons[lastIndex-1].navigation;
                    previousNavigation.mode = Navigation.Mode.Explicit;
                    previousNavigation.selectOnDown = choiceOptionButtons[lastIndex];
                    choiceOptionButtons[lastIndex-1].navigation = previousNavigation;
                }
            }
        }

        if (choiceOptionButtons.Count > 0) {
            choiceOptionButtons[0].Select();
        }
        else {
            ExecuteNextElement();
        }
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

        DialogueOption option = activeChoice.GetOptions(optionIndex);

        activeDialogueable.ExecuteFunction(option.GetFunction());

        if (activeDialogueable.freezeDialogue) {
            activeDialogueable.freezeDialogue = false;
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
