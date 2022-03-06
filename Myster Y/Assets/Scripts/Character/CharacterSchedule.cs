using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSchedule : MonoBehaviour {
    private CharacterAI aI;

    [SerializeField]private Vector3Int[] gridDestinations = new Vector3Int[24]; //Strings for testing
    private int currentDestination = 11; //Testing during midday

    private void Start() {
        TimeManager.instance.UpdateRoutinesEvent.AddListener(ChangeDestination);
        aI = GetComponent<CharacterAI>();
    }

    private void ChangeDestination() {
        currentDestination++;
        if (currentDestination >= 24) {
            currentDestination = 0;
        }

        aI.UpdatePath(gridDestinations[currentDestination]);
    }
}
