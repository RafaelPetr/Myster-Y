using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPuzzle : MonoBehaviour {
    public static FlowerPuzzle instance;
    [System.NonSerialized]public bool finished;

    [SerializeField]private GameObject dresserGroup;
    private dialogueable_hospital_dresser[] dressers;

    private string answer;
    [SerializeField]private string solution;

    private void Awake() {
        instance = this;
        transform.parent = null;
    }

    private void Start() {
        dressers = dresserGroup.GetComponentsInChildren<dialogueable_hospital_dresser>();

        foreach (dialogueable_hospital_dresser dresser in dressers) {
            dresser.OnPlaceEvent.AddListener(Verify);
        }
    }

    private void Verify() {
        answer = "";

        foreach (dialogueable_hospital_dresser dresser in dressers) {
            answer += dresser.GetValue(0);
            answer += dresser.GetValue(1);
        }

        if (answer.Equals(solution)) {
            finished = true;
        }
    }
}
