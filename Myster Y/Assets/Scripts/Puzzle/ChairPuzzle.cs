using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairPuzzle : MonoBehaviour {
    private List<Chair> chairs = new List<Chair>();
    [SerializeField]private GameObject[] chairGroups;

    private char[] answerArray;
    [SerializeField]private string solution;

    private void Start() {
        foreach (GameObject group in chairGroups) {
            chairs.AddRange(group.GetComponentsInChildren<Chair>());
        }

        answerArray = new char[chairs.Count]; 

        for (int i = 0; i < chairs.Count; i++) {
            answerArray[i] = chairs[i].GetDirection().ToString()[0];
            chairs[i].OnTurnEvent.AddListener(Verify);
        }
    }

    private void Verify(Chair chair) {
        int index = chairs.IndexOf(chair);

        answerArray[index] = chair.GetDirection().ToString()[0];

        string answer = new string(answerArray);

        if (answer.Equals(solution)) {
            //Player won
        }
    }
}
