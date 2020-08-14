using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ClueSense : MonoBehaviour {
    private bool sensing = false; 
    private float preparingSenseCounter = 0; 

    [System.NonSerialized]public UnityEvent OnSenseActivate; 
    [System.NonSerialized]public UnityEvent OnSenseDeactivate; 

    private Volume volume;
    private ColorAdjustments colorAdjustments;

    private void Awake() {
        OnSenseActivate = new UnityEvent();
        OnSenseDeactivate = new UnityEvent();
    }

    private void Start() {
        volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        volume.profile.TryGet(out colorAdjustments);
    }

    private void FixedUpdate() {
        
        if (Input.GetButton("ClueSense") && !sensing) {
            preparingSenseCounter += 0.5f;
            if (preparingSenseCounter >= 100) {
                sensing = true;
                OnSenseActivate.Invoke();
            }
        }

        else if (preparingSenseCounter > 0) {
            preparingSenseCounter -= 0.1f;
            if (preparingSenseCounter <= 0 && sensing) {
                sensing = false;
                OnSenseDeactivate.Invoke();
            }
        }

        colorAdjustments.contrast.value = preparingSenseCounter;
        colorAdjustments.saturation.value = -preparingSenseCounter;

    }
}
