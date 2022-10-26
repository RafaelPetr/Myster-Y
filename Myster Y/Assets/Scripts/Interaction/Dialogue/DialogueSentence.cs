using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSentence : DialogueElement {
    [TextArea(3,10)]private string text;
    private DialogueCharacter character;

    public override void Execute() {
        DialogueManager.instance.UpdateSentenceUI(this);
    }

    #region Getters

        public DialogueCharacter GetCharacter() {
            return character;
        }

        public string GetText() {
            return text;
        }

    #endregion

    #region Setters

        public void SetText(string value) {
            text = value;
        }

        public void SetCharacter(DialogueCharacter value) {
            character = value;
        }

    #endregion
}