using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Daylight : MonoBehaviour {
    private TimeManager timeManager;

    new private Light2D light;
    [SerializeField]private Gradient lightGradient;

    private void Start() {
        timeManager = TimeManager.instance;
        light = GetComponent<Light2D>();
    }

    private void Update() {
        light.intensity = -Mathf.Cos(timeManager.GetNormalizedDay() *2*Mathf.PI)/2 + 0.5f;
        light.color = lightGradient.Evaluate(timeManager.GetNormalizedDay());
    }
}
