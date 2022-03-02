using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSchedule : MonoBehaviour {
    [SerializeField]private string[] destinations = new string[24]; //Strings for testing
    private int currentDestination = 10; //Testing during midday

    private void Start() {
        TimeManager.instance.UpdateRoutinesEvent.AddListener(ChangeDestination);
    }

    private void ChangeDestination() {
        currentDestination++;
        if (currentDestination >= 24) {
            currentDestination = 0;
        }
        //Debug.Log(destinations[currentDestination]);
    }
}
