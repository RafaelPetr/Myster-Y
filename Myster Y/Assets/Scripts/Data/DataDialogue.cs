using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataDialogueList {
    [SerializeField]private List<DataDialogue> dialogues;

    #region Add

        public void AddDialogue(DataDialogue DataDialogue) {
            dialogues.Add(DataDialogue);
        }

    #endregion

    #region Remove

        public void RemoveDialogue(DataDialogue DataDialogue) {
            dialogues.Remove(DataDialogue);
        }

    #endregion

    #region Getters

        public List<DataDialogue> GetDialogues() {
            return dialogues;
        }

    #endregion
}


[System.Serializable]
public class DataDialogue {
    [SerializeField]private string key;
    [SerializeField]private List<DataSentence> sentences = new List<DataSentence>();
    [SerializeField]private DataChoice choice;
    [SerializeField]private List<int> order;

    public DataDialogue(Dialogue dialogue) {
        key = dialogue.GetKey();

        foreach (DialogueSentence sentence in dialogue.GetSentences()) {
            sentences.Add(new DataSentence(sentence));
        }

        if (dialogue.GetChoice() != null) {
            choice = new DataChoice(dialogue.GetChoice());
        }

        order = dialogue.GetOrder();
    }

    #region Getters

        public string GetKey() {
            return key;
        }

        public List<DataSentence> GetSentences() {
            return sentences;
        }

        public DataSentence GetSentences(int index) {
            return sentences[index];
        }

        public DataChoice GetChoice() {
            return choice;
        }

        public List<int> GetOrder() {
            return order;
        }

    #endregion
}

[System.Serializable]
public class DataSentence : LocalizationSentence {
    [SerializeField]private string character;

    public DataSentence(DialogueSentence sentence) : base(sentence) {
        if (sentence.GetCharacter() != null) {
            character = sentence.GetCharacter().name;
        }
    }

    #region Getters

        public string GetCharacter() {
            return character;
        }

    #endregion
}

[System.Serializable]
public class DataChoice {
    [SerializeField]private string context;
    [SerializeField]private List<DataOption> options = new List<DataOption>();

    public DataChoice(DialogueChoice choice) {
        context = choice.GetContext();

        foreach (DialogueOption option in choice.GetOptions()) {
            options.Add(new DataOption(option));
        }
    }

    #region Getters

        public string GetContext() {
            return context;
        }

        public List<DataOption> GetOptions() {
            return options;
        }

        public DataOption GetOptions(int index) {
            return options[index];
        }

    #endregion
}

[System.Serializable]
public class DataOption : LocalizationOption {
    [SerializeField]private string function;

    public DataOption(DialogueOption option) : base(option) {
        this.function = option.GetFunction();
    }

    #region Getters

        public string GetFunction() {
            return function;
        }

    #endregion
}