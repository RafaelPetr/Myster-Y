using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dialogueable_hospital_dresser : Dialogueable {
    [SerializeField]private Item flower;
    [SerializeField]private Item decorationLetter;
    private List<Item> flowers = new List<Item>();
    
    [SerializeField]private GameObject leftFlower;
    [SerializeField]private GameObject rightFlower;

    private char[] values = new char[]{'0', '0'};
    [System.NonSerialized]public UnityEvent OnPlaceEvent = new UnityEvent();

    public override void ResetKey() {
        base.ResetKey();
        key += "hospital_dresser_";
    }

    public override Dialogue DefineDialogue() {
        ResetKey();

        if (!FlowerPuzzle.instance.finished) {
            key += "unfinished";

            if (!Inventory.HasItem(decorationLetter)) {
                key += "_letter";
            }
            else {
                key += "_put_or_remove";
                SaveOptions();

                if (Inventory.HasItem(flower) && flowers.Count < 2) {
                    if (flowers.Count == 0) {
                        RemoveOption(1);
                    }
                }
                else {
                    RemoveOption(0);
                }
            }
        }
        else {
            key += "finished";
        }

        return dialogueDict[key];
    }

    public override void ExecuteFunction(string function) {
        if (function.Equals("Accept Letter")) {
            Inventory.AddItem(decorationLetter);
        }
        else if (function.Equals("Put")) {
            freezeDialogue = true;
            flowers.Add(flower);
            Inventory.RemoveItem(flower);

            if (!leftFlower.activeSelf && !rightFlower.activeSelf) {
                ResetOptions();

                key += "_side";
                StartDialogue(dialogueDict[key]);
            }
            else {
                if (!leftFlower.activeSelf) {
                    leftFlower.SetActive(true);
                    values[0] = '1';
                }
                else {
                    rightFlower.SetActive(true);
                    values[1] = '1';
                }

                OnPlaceEvent.Invoke();
            }
        }
        else if (function.Equals("Remove")) {
            foreach (Item item in flowers) {
                Inventory.AddItem(item);
            }

            leftFlower.SetActive(false);
            rightFlower.SetActive(false);

            values = new char[]{'0', '0'};

            flowers.Clear();
        }
        else if (function.Equals("Left") || function.Equals("Right")) {
            if (function.Equals("Left")) {
                leftFlower.SetActive(true);
                values[0] = '1';
            }
            else {
                rightFlower.SetActive(true);
                values[1] = '1';
            }

            OnPlaceEvent.Invoke();
        }
    }

    #region Getters

        public char[] GetValues() {
            return values;
        }

        public char GetValue(int index) {
            return values[index];
        }

    #endregion
}