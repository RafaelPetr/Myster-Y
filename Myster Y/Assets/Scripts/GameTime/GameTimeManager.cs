using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class GameTimeManager : MonoBehaviour {
    private static float day = 0.5f;
    private Transform clockHandHour;
    private Transform clockHandMin;
    public float realSecondsPerDayInGame = 600f;
    public Light2D ambientLight;
    public Gradient dayCycleGradient;
    private bool invokedChangeHour;

    [System.NonSerialized]public int currentHour;
    [System.NonSerialized]public int currentMinute;
    [System.NonSerialized]public UnityEvent OnChangeHour;

    void Awake() {
        clockHandHour = GameObject.Find("ClockHandHour").GetComponent<RectTransform>();
        clockHandMin = GameObject.Find("ClockHandMin").GetComponent<RectTransform>();
        OnChangeHour = new UnityEvent();
    }

    // Update is called once per frame
    void Update() {
        day += Time.deltaTime / realSecondsPerDayInGame;
        float dayNormalized = day % 1f;

        if (ambientLight != null) {
            ambientLight.intensity = -Mathf.Cos(dayNormalized *2*Mathf.PI)/2 + 0.5f;
            ambientLight.color = dayCycleGradient.Evaluate(dayNormalized);
        }

        float rotationDegreesPerDay = 720f;
        clockHandHour.eulerAngles = new Vector3(0,0,-dayNormalized*rotationDegreesPerDay);

        float hoursPerDay = 24f;
        clockHandMin.eulerAngles = new Vector3(0,0,-dayNormalized*rotationDegreesPerDay*hoursPerDay);

        float minutesPerHour = 60f;

        currentHour = (int)Mathf.Floor(dayNormalized*hoursPerDay);
        currentMinute = (int)Mathf.Floor(((dayNormalized*hoursPerDay)%1f)*minutesPerHour);

        string hoursString = currentHour.ToString("00");
        string minutesString = currentMinute.ToString("00");

        if (currentMinute == 0 && !invokedChangeHour) {
            OnChangeHour.Invoke();
            invokedChangeHour = true;
        }
        else if (currentMinute == 1) {
            invokedChangeHour = false;
        }

        //Debug.Log(hoursString+":"+minutesString);
    }
}
