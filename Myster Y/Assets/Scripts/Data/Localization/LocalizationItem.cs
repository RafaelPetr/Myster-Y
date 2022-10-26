using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationItem {
    [SerializeField]private string key;

    [SerializeField]private string name;
    [SerializeField]private List<string> description;

    public LocalizationItem(Item item) {
        this.key = item.GetKey();

        this.name = item.GetName();
        this.description = item.GetDescription();
    }

    #region Getters

        public string GetKey() {
            return key;
        }

        public string GetName() {
            return name;
        }

        public List<string> GetDescription() {
            return description;
        }

        public string GetDescription(int index) {
            return description[index];
        }

    #endregion
}
