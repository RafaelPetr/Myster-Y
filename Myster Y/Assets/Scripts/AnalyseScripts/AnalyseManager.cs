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

    public GameObject detective;
    public GameObject analyseItemScene;

    public GameObject itemAnalyse;

    private Animator detectiveAnimator;
    private Animator handAnimator;

    private int panelOpen;

    private bool analysing;

    private void Start() {
        detectiveAnimator = detective.GetComponent<Animator>();
        handAnimator = analyseItemScene.GetComponentInChildren<Animator>();
    }

    public void StartAnalyse(Item item) {
        analysing = true;
        panelOpen = (int)detectiveAnimator.GetFloat("InventoryPanel");
        analyseItemScene.transform.SetParent(null);

        detective.SetActive(false);
        analyseItemScene.SetActive(true);
        InventoryUI.instance.UpdateUI(-1);

        itemAnalyse.GetComponent<SpriteRenderer>().sprite = item.analyseSprite;
    }

    void Update() {
        if (Input.GetButtonDown("Back") && analysing) {
            StartCoroutine(StopAnalysing());
        }
    }

    IEnumerator StopAnalysing() {
        analysing = false;

        handAnimator.SetTrigger("Exit");

        yield return new WaitForSeconds(1);
        analyseItemScene.SetActive(false);
        detective.SetActive(true);

        analyseItemScene.transform.SetParent(detective.transform);

        detectiveAnimator.SetFloat("InventoryPanel",panelOpen);
        PlayerInventory.instance.OpenPanel(panelOpen);

    }
}
