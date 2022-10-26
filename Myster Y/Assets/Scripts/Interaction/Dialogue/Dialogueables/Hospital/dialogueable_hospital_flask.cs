using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueable_hospital_flask : Dialogueable {
    private SpriteRenderer flaskSprite;
    [SerializeField]private SpriteRenderer liquidSprite;

    private List<int> consumedLiquids = new List<int>();

    [SerializeField]private Flask flaskItem;

    public override void Start() {
        base.Start();

        flaskSprite = GetComponent<SpriteRenderer>();
        if (flaskItem.GetCounter() >= 3) {
            flaskItem.ResetMixture();
        }
    }

    private void FixedUpdate() {
        liquidSprite.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
    }

    public override void ResetKey() {
        base.ResetKey();
        key += "hospital_flask_";
    }

    public override Dialogue DefineDialogue() {
        ResetKey();

        if (!LabPuzzle.instance.GetFinished()) {
            key += "unfinished";

            if (flaskItem.GetCounter() >= 3) {
                if (!Inventory.HasItem(flaskItem)) {
                    key += "_pick_or_redo";
                }
                else {
                    key += "_redo";
                }
            }
        }
        else {
            key += "finished";
        }
        return dialogueDict[key];
    }

    public override void ExecuteFunction(string function) {
        if (function == "Mixture") {
            freezeDialogue = true;

            key += "_mixture";
            SaveOptions();

            for (int i = 0; i < consumedLiquids.Count; i++) {
                RemoveOption(consumedLiquids[i]);
            }

            StartDialogue(dialogueDict[key]);
        }
        else if (int.TryParse(function, out int number)) {
            if (flaskItem.GetCounter() < 3) {
                RemoveOption(number);

                consumedLiquids.Add(number);
                flaskItem.AddMixture(number);
                LabPuzzle.instance.SetLiquid(number, false);

                if (flaskItem.GetCounter() < 3) {
                    freezeDialogue = true;
                    StartDialogue(dialogueDict[key]);
                }
                else {
                    ResetOptions();
                    liquidSprite.enabled = true;
                }
            }
        }
        else if (function == "Pickup") {
            Inventory.AddItem(flaskItem);
            flaskSprite.enabled = false;
            liquidSprite.enabled = false;
        }
        else if (function == "Redo") {
            if (!LabPuzzle.instance.GetConsume()) {
                for (int i = 0; i < flaskItem.GetCounter(); i++) {
                    LabPuzzle.instance.SetLiquid(flaskItem.GetLiquid(i), true);
                    consumedLiquids.Remove(flaskItem.GetLiquid(i));
                }
            }
            
            LabPuzzle.instance.SetConsume(false);

            Inventory.RemoveItem(flaskItem);
            flaskSprite.enabled = true;
            liquidSprite.enabled = false;

            ResetKey();
            flaskItem.ResetMixture();

            key += "unfinished";
            ExecuteFunction("Mixture");
        }
    }
}
