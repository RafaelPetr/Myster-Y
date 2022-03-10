using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSchedule : MonoBehaviour {
    private CharacterAI aI;
    private TimeManager timeManager;
    [SerializeField]private Vector3Int[] gridDestinations = new Vector3Int[24]; //Strings for testing

    private void Start() {
        timeManager = TimeManager.instance;
        timeManager.UpdateRoutinesEvent.AddListener(ChangeDestination);
        
        aI = GetComponent<CharacterAI>();
    }

    private void ChangeDestination() {
        aI.UpdatePath(gridDestinations[timeManager.GetNormalizedHour()]);
    }
}
