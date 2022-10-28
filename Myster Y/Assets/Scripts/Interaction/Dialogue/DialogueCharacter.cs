using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptable_character_", menuName = "Dialogue/Character")]
public class DialogueCharacter : ScriptableObject {
    [SerializeField]new private string name;
    [SerializeField]private Sprite icon;

    #region Getters

        public string GetName() {
            return name;
        }

        public Sprite GetIcon() {
            return icon;
        }

    #endregion

    #region Setters

        public void SetName(string value) {
            name = value;
        }

        public void SetIcon(Sprite value) {
            icon = value;
        }

    #endregion
}
