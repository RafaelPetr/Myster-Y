using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice : DialogueElement {
    [TextArea(3,10)]private string context;
    private List<DialogueOption> options = new List<DialogueOption>();

    public override void Execute() {
        DialogueManager.instance.UpdateChoiceUI(this);
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

        public string GetContext() {
            return context;
        }

        public List<DialogueOption> GetOptions() {
            return options;
        }

        public DialogueOption GetOptions(int index) {
            return options[index];
        }

    #endregion

    #region Setters

        public void SetContext(string value) {
            context = value;
        }

    #endregion
}