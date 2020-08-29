using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensableObject : MonoBehaviour {
    [SerializeField]private ParticleSystem particleSystem;
    [SerializeField]private ClueSense clueSense;

    private void Start() {
        particleSystem.enableEmission = false;
        clueSense.OnSenseActivate.AddListener(ShowClue);
        clueSense.OnSenseDeactivate.AddListener(HideClue);
    }

    private void ShowClue() {
        particleSystem.enableEmission = true;
    }

    private void HideClue() {
        particleSystem.enableEmission = false;
    }
}
