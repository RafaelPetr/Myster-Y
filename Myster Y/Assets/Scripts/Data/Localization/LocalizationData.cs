using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationData {
    [SerializeField]private List<LocalizationDialogue> dialogues = new List<LocalizationDialogue>();
    [SerializeField]private List<LocalizationItem> items = new List<LocalizationItem>();
    [SerializeField]private List<LocalizationText> texts = new List<LocalizationText>();

    #region Add

        public void AddDialogue(LocalizationDialogue dialogue) {
            dialogues.Add(dialogue);
        }

        public void AddItem(LocalizationItem item) {
            items.Add(item);
        }

        public void AddText(LocalizationText text) {
            texts.Add(text);
        }

    #endregion

    #region Remove

        public void RemoveDialogue(LocalizationDialogue dialogue) {
            dialogues.Remove(dialogue);
        }

        public void RemoveItem(LocalizationItem item) {
            items.Remove(item);
        }

        public void RemoveText(LocalizationText text) {
            texts.Remove(text);
        }

    #endregion

    #region Getters

        public List<LocalizationDialogue> GetDialogues() {
            return dialogues;
        }

        public LocalizationDialogue GetDialogues(int index) {
            return dialogues[index];
        }

        public List<LocalizationItem> GetItems() {
            return items;
        }

        public LocalizationItem GetItems(int index) {
            return items[index];
        }

        public List<LocalizationText> GetTexts() {
            return texts;
        }

        public LocalizationText GetTexts(int index) {
            return texts[index];
        }

    #endregion
}