using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyseManager : MonoBehaviour {
    #region Singleton
    public static AnalyseManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    #endregion

    public GameObject mainScene;
    public GameObject analyseItemScene;

    public Animator detectiveAnimator;
    private float panelOpen;

    private bool analysing;

    public void StartAnalyse(Item item) {
        panelOpen = detectiveAnimator.GetFloat("InventoryPanel");
        analysing = true;
        analyseItemScene.SetActive(true);
        mainScene.SetActive(false);
    }

    void Update() {
        if (Input.GetButtonDown("Back") && analysing) {
            mainScene.SetActive(true);
            analyseItemScene.SetActive(false);
            detectiveAnimator.SetFloat("InventoryPanel",panelOpen);
            detectiveAnimator.Play("OpenInventoryPanel", 0, 1);
            analysing = false;
        }
    }
}
