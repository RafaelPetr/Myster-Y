using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueDataGroup {
    [SerializeField]private List<DialogueData> dialogues;

    #region Getters

        public List<DialogueData> GetDialogues() {
            return dialogues;
        }

    #endregion
}


[System.Serializable]
public class DialogueData {
    [SerializeField]private string key;
    [SerializeField]private List<SentenceData> sentences = new List<SentenceData>();
    [SerializeField]private ChoiceData choice;

    public DialogueData(Dialogue dialogue) {
        key = dialogue.GetKey();

        foreach (DialogueSentence sentence in dialogue.GetSentences()) {
            sentences.Add(new SentenceData(sentence));
        }

        if (dialogue.GetChoice() != null) {
            choice = new ChoiceData(dialogue.GetChoice());
        }
    }

    #region Getters

        public string GetKey() {
            return key;
        }

        public List<SentenceData> GetSentences() {
            return sentences;
        }

        public SentenceData GetSentences(int index) {
            return sentences[index];
        }

        public ChoiceData GetChoice() {
            return choice;
        }

    #endregion
}

[System.Serializable]
public class SentenceData : LocalizationSentence {
    [SerializeField]private string character;

    public SentenceData(DialogueSentence sentence) : base(sentence) {
        this.character = sentence.GetCharacter().name;
    }

    #region Getters

        public string GetCharacter() {
            return character;
        }

    #endregion
}

[System.Serializable]
public class ChoiceData {
    [SerializeField]private string context;
    [SerializeField]private List<LocalizationOption> options = new List<LocalizationOption>();

    public ChoiceData(DialogueChoice choice) {
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
public class OptionData : LocalizationOption {
    [SerializeField]private string function;

    public OptionData(DialogueOption option) : base(option) {
        this.function = option.GetFunction();
    }

    #region Getters

        public string GetFunction() {
            return function;
        }

    #endregion
}