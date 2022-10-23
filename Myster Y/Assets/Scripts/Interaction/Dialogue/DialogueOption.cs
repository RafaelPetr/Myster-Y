using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption {
    private string text;
    private string function;

    private int locIndex;

    public void LocalizeText(string key) {
        text = LocalizationManager.instance.GetLocalizedValue(key,locIndex);
    }

    #region Getters

        public string GetText() {
            return text;
        }

        public string GetFunction() {
            return function;
        }

        public int GetLocIndex() {
            return locIndex;
        }

    #endregion

    #region Setters

        public void SetText(string value) {
            text = value;
        }

        public void SetFunction(string value) {
            function = value;
        }

        public void SetLocIndex(int value) {
            locIndex = value;
        }

    #endregion
}
