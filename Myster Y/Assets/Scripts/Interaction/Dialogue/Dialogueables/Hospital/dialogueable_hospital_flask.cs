using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueable_hospital_flask : Dialogueable {
    private SpriteRenderer flaskSprite;
    [SerializeField]private SpriteRenderer liquidSprite;

    [SerializeField]private Flask flaskItem;

    public override void Start() {
        base.Start();

        flaskSprite = GetComponent<SpriteRenderer>();
        if (!Inventory.HasItem(flaskItem)) {
            flaskItem.mixture.Clear();
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

        if (!LabPuzzle.instance.finished) {
            key += "unfinished";

            if (flaskItem.mixture.Count == 3) {
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
            StartDialogue(dialogueDict[key]);
        }
        else if (int.TryParse(function, out int number)) {
            if (flaskItem.mixture.Count < 3) {
                RemoveOption(number);

                flaskItem.mixture.Add(number);

                if (flaskItem.mixture.Count < 3) {
                    freezeDialogue = true;
                    StartDialogue(dialogueDict[key]);
                }
                else {
                    ResetOptions();
                    liquidSprite.gameObject.SetActive(true);
                }
            }
        }
        else if (function == "Pickup") {
            Inventory.AddItem(flaskItem);
            flaskSprite.enabled = false;
            liquidSprite.enabled = false;
        }
        else if (function == "Redo") {
            Inventory.RemoveItem(flaskItem);
            flaskSprite.enabled = true;
            liquidSprite.enabled = true;

            ResetKey();
            flaskItem.mixture.Clear();

            key += "unfinished";
            ExecuteFunction("Mixture");
        }
    }
}
