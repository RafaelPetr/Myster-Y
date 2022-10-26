using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption {
    private string text;
    private string function;

    #region Getters

        public string GetText() {
            return text;
        }

        public string GetFunction() {
            return function;
        }

    #endregion

    #region Setters

        public void SetText(string value) {
            text = value;
        }

        public void SetFunction(string value) {
            function = value;
        }

    #endregion
}
