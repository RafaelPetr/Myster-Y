using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptable_item_", menuName = "Item/Item")]
public class Item : ScriptableObject {
    private string key;

    private new string name;
    private List<string> description;

    private Sprite icon;
    private Sprite analysisImage;

    public void Analyze() {
        AnalysisManager.instance.StartAnalysis(this);
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

        public void SetIcon(Sprite value) {
            icon = value;
        }

        public void SetAnalysisImage(Sprite value) {
            analysisImage = value;
        }

    #endregion
}