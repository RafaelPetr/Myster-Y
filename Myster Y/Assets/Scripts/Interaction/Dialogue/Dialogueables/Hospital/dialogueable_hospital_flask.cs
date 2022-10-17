using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueable_hospital_flask : Dialogueable {
    private int mixtureCounter = 0;

    public override void ResetKey() {
        base.ResetKey();
        key += "hospital_flask_";
    }

    public override Dialogue DefineDialogue() {
        //if (!LabPuzzle.instance.finished) {
            key += "unfinished";
        //}
        //else {
            //key += "finished";
        //}
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
            if (mixtureCounter < 3) {
                mixtureCounter++;
                Debug.Log(number);

                RemoveOption(number);
                if (mixtureCounter < 3) {
                    freezeDialogue = true;
                    StartDialogue(dialogueDict[key]);
                }
            }
            else {
                ResetOptions();
                mixtureCounter = 0;
            }
        }
    }
}
