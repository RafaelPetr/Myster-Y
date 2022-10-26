using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationText {
    [SerializeField]private string key;
    [SerializeField]private string value;

    #region Getters

        public string GetKey() {
            return key;
        }

        public string GetValue() {
            return value;
        }

    #endregion
}
