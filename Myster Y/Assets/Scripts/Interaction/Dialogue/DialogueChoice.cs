using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice : DialogueElement {
    [SerializeField]private bool enable;
    private int locIndex;

    [TextArea(3,10)]private string context;
    private List<DialogueOption> options = new List<DialogueOption>();

    public override void Execute() {
        DialogueManager.instance.UpdateChoiceUI(this);
    }

    public void LocalizeText(string key) {
		context = LocalizationManager.instance.GetLocalizedValue(key,locIndex);

        foreach (DialogueOption option in options) {
            option.LocalizeText(key);
        }
    }

    #region Add

        public void AddOption() {
            options.Add(new DialogueOption());
        }

    #endregion

    #region Remove

        public void RemoveOption(int index) {
            options.RemoveAt(index);
        }

    #endregion

    #region Getters

        public bool GetEnable() {
            return enable;
        }

        public int GetLocIndex() {
            return locIndex;
        }

        public string GetContext() {
            return context;
        }

        public List<DialogueOption> GetOptions() {
            return options;
        }

        public DialogueOption GetOption(int index) {
            return options[index];
        }

    #endregion

    #region Setters

        public void SetEnable(bool value) {
            enable = value;
        }

        public void SetLocIndex(int value) {
            locIndex = value;
        }

        public void SetContext(string value) {
            context = value;
        }

    #endregion
}
