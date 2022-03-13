using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Schedule", fileName = "scriptable_schedule_")]
public class Schedule : ScriptableObject {
    [SerializeField]private Destination[] destinantions = new Destination[24];

    public Destination GetDestination(int index) {
        return destinantions[index];
    }
}