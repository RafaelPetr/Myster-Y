using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour {
    public static TimeManager instance;

    private bool pauseTime;
    private float day = 0.5f; //Testing during midday
    private float normalizedDay;
    private float hour;
    private float minute;

    private static float realSecondsPerDay = 60f;
    private static float hoursPerDay = 24f;
    private static float minutesPerHour = 60f;

    private float lastHour;
    private bool updateRoutines;
    private int minutesToUpdate;

    public delegate void PauseTimeEvent(bool value);
    [System.NonSerialized]public PauseTimeEvent pauseTimeEvent;
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
        if (!pauseTime) {
            day += Time.deltaTime / realSecondsPerDay;
            normalizedDay = day % 1f;

            hour = Mathf.Floor(normalizedDay*hoursPerDay);
            minute = Mathf.Floor(((normalizedDay*hoursPerDay)%1f)*minutesPerHour);

            UpdateRoutines();
        }
    }

    private void UpdateRoutines() {
        if (updateRoutines && lastHour != hour) { //Updates routines every change of hour
            UpdateRoutinesEvent.Invoke();

            lastHour = hour;
            updateRoutines = false;
        }
        else {
            updateRoutines = true;
        }
    }

    public bool GetPauseTime() {
        return pauseTime;
    }

    public void SetPauseTime(bool value) {
        pauseTime = value;
        pauseTimeEvent.Invoke(value);
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
