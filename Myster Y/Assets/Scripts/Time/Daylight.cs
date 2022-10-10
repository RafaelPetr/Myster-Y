using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Daylight : MonoBehaviour {
    new private Light2D light;
    private Gradient lightGradient;

    private void Start() {
        light = GetComponent<Light2D>();
        lightGradient = GenerateDaylightGradient();
    }

    private void Update() {
        light.intensity = -Mathf.Cos(TimeManager.instance.GetNormalizedDay() *2*Mathf.PI)/2 + 0.5f;
        light.color = lightGradient.Evaluate(TimeManager.instance.GetNormalizedDay());
    }

    private Gradient GenerateDaylightGradient() {
        Gradient gradient = new Gradient();

        GradientColorKey[] colorKey = new GradientColorKey[3];
        colorKey[0].color = new Color(0.43f,0.58f,0.78f); //Blue (Day)
        colorKey[0].time = 0.25f;

        colorKey[1].color = new Color(0.74f,0.52f,0.31f); //Orange (Afternoon)
        colorKey[1].time = 0.6f;

        colorKey[2].color = new Color(0.43f,0.41f,0.74f); //Blue (Night)
        colorKey[2].time = 0.85f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 0.25f;

        alphaKey[1].alpha = 1f;
        alphaKey[1].time = 0.6f;

        alphaKey[2].alpha = 1f;
        alphaKey[2].time = 0.85f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }
}
