using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataItemList {
    [SerializeField]private List<DataItem> items = new List<DataItem>();

    #region Add

        public void AddItem(DataItem item) {
            items.Add(item);
        }

    #endregion

    #region Remove

        public void RemoveItem(DataItem item) {
            items.Remove(item);
        }

    #endregion

    #region Getters

        public List<DataItem> GetItems() {
            return items;
        }

        public DataItem GetItems(int index) {
            return items[index];
        }

    #endregion
}

[System.Serializable]
public class DataItem : LocalizationItem {
    [SerializeField]private string icon;
    [SerializeField]private string analysisImage;

    public DataItem(Item item) : base(item) {
        Sprite icon = item.GetIcon();
        Sprite analysisImage = item.GetAnalysisImage();

        if (icon != null) {
            this.icon = icon.name;
        }

        if (analysisImage != null) {
            this.analysisImage = analysisImage.name;
        }
    }

    #region Getters

        public string GetIcon() {
            return icon;
        }

        public string GetAnalysisImage() {
            return analysisImage;
        }

    #endregion
}
