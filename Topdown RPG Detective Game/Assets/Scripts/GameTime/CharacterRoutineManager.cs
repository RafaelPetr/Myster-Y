﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterRoutineManager : MonoBehaviour {
    public Vector3[] destinations = new Vector3[24];
    public Vector3 currentDestination;
    private GameTimeManager timeManager;

    private void Awake() {
        timeManager = GameObject.Find("GameTimeManager").GetComponent<GameTimeManager>();
    }

    private void Start() {
        timeManager.OnChangeHour.AddListener(ChangeRoutine);
    }

    private void ChangeRoutine() {
        currentDestination = destinations[timeManager.currentHour];
        dontReallyKnowStill();
    }

    public abstract void dontReallyKnowStill();
    
}
