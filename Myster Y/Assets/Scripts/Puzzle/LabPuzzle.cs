using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPuzzle : MonoBehaviour {
    public static LabPuzzle instance;

    [SerializeField]private List<SpriteRenderer> liquids = new List<SpriteRenderer>();

    private int correctAnswers;
    private bool finished;
    private bool consume;

    private void Awake() {
        instance = this;
        transform.parent = null;
    }

    public void Verify(dialogueable_hospital_microscope microscope) {
        Flask flask = (Flask)Inventory.GetItem("scriptable_item_flask");

        if (flask != null) {
            string answer = "";

            foreach (int liquid in flask.GetMixture()) {
                answer += liquid.ToString();
            }

            if (answer.Equals(microscope.GetSolution())) {
                microscope.SetFinished(true);

                consume = true;
                correctAnswers++;

                if (correctAnswers == 3) {
                    finished = true;
                }
            }
        }
    }

    #region Getters

        public bool GetFinished() {
            return finished;
        }

        public bool GetConsume() {
            return consume;
        }

    #endregion

    #region Setters

        public void SetConsume(bool value) {
            consume = value;
        }

        public void SetLiquid(int number, bool value) {
            liquids[number].enabled = value;
        }

    #endregion
}
