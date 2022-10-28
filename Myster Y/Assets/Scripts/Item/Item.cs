using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptable_item_", menuName = "Item/Item")]
public class Item : ScriptableObject {
    [SerializeField]private string key;

    [SerializeField]private new string name;
    [SerializeField]private List<string> description = new List<string>();

    [SerializeField]private Sprite icon;
    [SerializeField]private Sprite analysisImage;

    public void LoadData(DataItem item) {
        name = item.GetName();
        description = item.GetDescription();

        if (item.GetIcon() != string.Empty) {
            icon = Resources.Load<Sprite>("Sprites/Items/Icons/" + item.GetIcon());
        }

        if (item.GetAnalysisImage() != string.Empty) {
            analysisImage = Resources.Load<Sprite>("Sprites/Items/Analysis/" + item.GetAnalysisImage());
        }
    }

    public void Analyze() {
        AnalysisManager.instance.StartAnalysis(this);
    }

    #region Add

        public void AddDescription(string value) {
            description.Add(value);
        }

    #endregion

    #region Remove

        public void RemoveDescription(string value) {
            description.Remove(value);
        }

        public void RemoveDescription(int index) {
            description.RemoveAt(index);
        }

    #endregion

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

        public Sprite GetIcon() {
            return icon;
        }

        public Sprite GetAnalysisImage() {
            return analysisImage;
        }

    #endregion

    #region Setters

        public void SetKey(string value) {
            key = value;
        }

        public void SetName(string value) {
            name = value;
        }

        public void SetDescription(List<string> value) {
            description = value;
        }

        public void SetDescription(int index, string value) {
            description[index] = value;
        }

        public void SetIcon(Sprite value) {
            icon = value;
        }

        public void SetAnalysisImage(Sprite value) {
            analysisImage = value;
        }

    #endregion
}