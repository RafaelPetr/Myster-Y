using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour {
    public static TimeManager instance;

    private float day = 0.5f;
    private float normalizedDay;
    private float hour;
    private float minute;

    private static float realSecondsPerDay = 10f;
    private static float hoursPerDay = 24f;
    private static float minutesPerHour = 60f;

    private bool updateRoutines;
    private int minutesToUpdate;
    [System.NonSerialized]public UnityEvent UpdateRoutinesEvent;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        UpdateRoutinesEvent = new UnityEvent();
    }

    private void Update() {
        UpdateTime();
    }

    private void UpdateTime() {
        day += Time.deltaTime / realSecondsPerDay;
        normalizedDay = day % 1f;

        hour = Mathf.Floor(normalizedDay*hoursPerDay);
        minute = Mathf.Floor(((normalizedDay*hoursPerDay)%1f)*minutesPerHour);

        UpdateRoutines();
    }

    private void UpdateRoutines() {
        if (updateRoutines && minute == 0) { //Update routines every 1 hour
            UpdateRoutinesEvent.Invoke();
            updateRoutines = false;
        }
        else {
            updateRoutines = true;
        }
    }

    public float GetDay() {
        return day;
    }

    public float GetNormalizedDay() {
        return normalizedDay;
    }

    public float GetHour() {
        return hour;
    }

    public float GetMinute() {
        return minute;
    }
}
