using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPuzzle : MonoBehaviour {
    public static LabPuzzle instance;
    [System.NonSerialized]public bool finished;

    private void Awake() {
        instance = this;
        transform.parent = null;
    }

    public void Verify(dialogueable_hospital_microscope microscope) {
        Flask flask = (Flask)Inventory.GetItem("scriptable_item_flask");

        if (flask != null) {
            string answer = "";

            foreach (int liquid in flask.mixture) {
                answer += liquid.ToString();
            }
            Debug.Log(answer);

            if (answer.Equals(microscope.solution)) {
                Debug.Log("Ganhamo");
                microscope.finished = true;
            }
            else {
                Debug.Log("Perdemo");
            }
        }
    }
}
