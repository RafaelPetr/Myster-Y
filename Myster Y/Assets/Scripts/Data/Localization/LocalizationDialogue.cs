using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationDialogue { //Used when changing localization texts
    [SerializeField]private string key;
    [SerializeField]private List<LocalizationSentence> sentences = new List<LocalizationSentence>();
    [SerializeField]private LocalizationChoice choice;

    public LocalizationDialogue(Dialogue dialogue) {
        key = dialogue.GetKey();

        foreach (DialogueSentence sentence in dialogue.GetSentences()) {
            sentences.Add(new LocalizationSentence(sentence));
        }

        if (dialogue.GetChoice() != null) {
            choice = new LocalizationChoice(dialogue.GetChoice());
        }
    }

    #region Getters

        public string GetKey() {
            return key;
        }

        public List<LocalizationSentence> GetSentences() {
            return sentences;
        }

        public LocalizationSentence GetSentences(int index) {
            return sentences[index];
        }

        public LocalizationChoice GetChoice() {
            return choice;
        }

    #endregion
}

[System.Serializable]
public class LocalizationSentence {
    [SerializeField]private string text;

    public LocalizationSentence(DialogueSentence sentence) {
        this.text = sentence.GetText();
    }

    #region Getters

        public string GetText() {
            return text;
        }

    #endregion
}

[System.Serializable]
public class LocalizationChoice {
    [SerializeField]private string context;
    [SerializeField]private List<LocalizationOption> options = new List<LocalizationOption>();

    public LocalizationChoice(DialogueChoice choice) {
        this.context = choice.GetContext();

        foreach (DialogueOption option in choice.GetOptions()) {
            options.Add(new LocalizationOption(option));
        }
    }

    #region Getters

        public string GetContext() {
            return context;
        }

        public List<LocalizationOption> GetOptions() {
            return options;
        }

        public LocalizationOption GetOptions(int index) {
            return options[index];
        }

    #endregion
}

[System.Serializable]
public class LocalizationOption {
    [SerializeField]private string text;

    public LocalizationOption(DialogueOption option) {
        this.text = option.GetText();
    }

    #region Getters

        public string GetText() {
            return text;
        }

    #endregion
}