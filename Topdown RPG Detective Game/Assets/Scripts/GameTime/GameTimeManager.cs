using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameTimeManager : MonoBehaviour {
    private static float day = 0.5f;
    private Transform clockHandMin;
    public float realSecondsPerDayInGame = 10800;
    public Light2D ambientLight;
    public Gradient dayCycleGradient;

    void Awake() {
        clockHandMin = transform.Find("ClockHandMin");
    }

    // Update is called once per frame
    void Update() {
        day += Time.deltaTime / realSecondsPerDayInGame;
        float dayNormalized = day % 1f;
        if (ambientLight != null) {
            ambientLight.intensity = -Mathf.Cos(dayNormalized *2*Mathf.PI)/2 + 0.5f;
            ambientLight.color = dayCycleGradient.Evaluate(dayNormalized);
        }

        float hoursPerDay = 24f;
        float minutesPerHour = 60f;

        string hoursString = Mathf.Floor(dayNormalized*hoursPerDay).ToString("00");
        string minutesString = Mathf.Floor(((dayNormalized*hoursPerDay)%1f)*minutesPerHour).ToString("00");

        //Debug.Log(hoursString+":"+minutesString);
    }
}
