using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueable_hospital_microscope : Dialogueable {
    private bool seeing;

    [System.NonSerialized]public bool finished;
    public string solution;

    public override void ResetKey() {
        base.ResetKey();
        key += "hospital_microscope_";
    }

    public override Dialogue DefineDialogue() {
        ResetKey();

        if (!finished) {
            key += "unfinished";

            if (!seeing) {
                key += "_see";
            }
            else {
                if (Inventory.HasItem("scriptable_item_flask")) {
                    key += "_use";
                }
                else {
                    seeing = false;
                    key += "_cancel";
                }
            }
        }
        else {
            key += "finished";
        }

        return dialogueDict[key];
    }

    public override void ExecuteFunction(string function) {
        if (function == "See") {
            seeing = true;
            //LabPuzzle.instance.SeeSample(solution);
        }
        else if (function == "Use") {
            seeing = false;
            LabPuzzle.instance.Verify(this);

            if (!finished) {
                freezeDialogue = true;
                
                key += "_failed";
                StartDialogue(dialogueDict[key]);
            }
        }
    }
}
