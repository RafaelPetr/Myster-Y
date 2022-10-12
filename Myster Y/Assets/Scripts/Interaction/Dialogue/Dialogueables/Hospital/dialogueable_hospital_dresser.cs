using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueable_hospital_dresser : Dialogueable {
    [SerializeField]private Item flower;
    [SerializeField]private Item decorationLetter;
    private List<Item> flowers = new List<Item>();
    
    [SerializeField]private GameObject rightFlower;
    [SerializeField]private GameObject leftFlower;

    public override void Awake() {
        base.Awake();

        key += "hospital_dresser_";
    }

    public override Dialogue DefineDialogue() {
        //if (!DresserPuzzle.finished) {
            if (!Inventory.FindItem(decorationLetter)) {
                return dialogueDict[key + "unfinished"];
            }
            if (Inventory.FindItem(flower) && flowers.Count == 0) {
                return dialogueDict[key + "unfinished_put"];
            }
            if (Inventory.FindItem(flower) && flowers.Count == 1) {
                return dialogueDict[key + "unfinished_put_or_remove"];
            }
            if (flowers.Count == 2) {
                return dialogueDict[key + "unfinished_remove"];
            }
        //}

        return dialogueDict[key + "finished"];
    }

    public override void ExecuteFunction(string function) {
        if (function.Equals("Accept Letter")) {
            Inventory.AddItem(decorationLetter);
        }
        else if (function.Equals("Put")) {
            flowers.Add(flower);
            Inventory.RemoveItem(flower);

            if (!leftFlower.activeSelf) {
                leftFlower.SetActive(true);
            }
            else {
                rightFlower.SetActive(true);
            }
        }
        else if (function.Equals("Remove")) {
            foreach (Item item in flowers) {
                Inventory.AddItem(item);
            }

            leftFlower.SetActive(false);
            rightFlower.SetActive(false);

            flowers.Clear();
        }
        else if (function.Equals("Left") || function.Equals("Right")) {
            flowers.Add(flower);
            Inventory.RemoveItem(flower);

            if (function.Equals("Left")) {
                leftFlower.SetActive(true);
            }
            else {
                rightFlower.SetActive(true);
            }
        }
    }
}
