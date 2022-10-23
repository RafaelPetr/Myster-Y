using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSentence : DialogueElement {
    [TextArea(3,10)]private string text;
    private DialogueCharacter character;

    private int locIndex;
    
    public override void Execute() {
        DialogueManager.instance.UpdateSentenceUI(this);
    }

    public void LocalizeText(string key) {
		text = LocalizationManager.instance.GetLocalizedValue(key, locIndex);
    }

    #region Getters

        public DialogueCharacter GetCharacter() {
            return character;
        }

        public string GetText() {
            return text;
        }

        public int GetLocIndex() {
            return locIndex;
        }

    #endregion

    #region Setters

        public void SetText(string value) {
            text = value;
        }

        public void SetCharacter(DialogueCharacter value) {
            character = value;
        }

        public void SetLocIndex(int value) {
            locIndex = value;
        }

    #endregion
}