using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeManager : MonoBehaviour {
    #region Singleton
    public static AnalyzeManager instance;

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
    public GameObject analyzeItemScene;

    public GameObject itemAnalyze;

    private Animator detectiveAnimator;
    private Animator handAnimator;

    private int panelOpen;

    private bool analyzing;

    private void Start() {
        detectiveAnimator = detective.GetComponent<Animator>();
        handAnimator = analyzeItemScene.GetComponentInChildren<Animator>();
    }

    public void StartAnalyze(Item item) {
        analyzing = true;
        panelOpen = (int)detectiveAnimator.GetFloat("InventoryPanel");
        analyzeItemScene.transform.SetParent(null);

        detective.SetActive(false);
        analyzeItemScene.SetActive(true);
        InventoryUI.instance.UpdateUI(-1);

        itemAnalyze.GetComponent<SpriteRenderer>().sprite = item.analyzable.sprite;
    }

    void Update() {
        if (Input.GetButtonDown("Back") && analyzing) {
            StartCoroutine(StopAnalyzing());
        }
    }

    IEnumerator StopAnalyzing() {
        analyzing = false;

        handAnimator.SetTrigger("Exit");

        yield return new WaitForSeconds(1);
        analyzeItemScene.SetActive(false);
        detective.SetActive(true);

        analyzeItemScene.transform.SetParent(detective.transform);

        detectiveAnimator.SetFloat("InventoryPanel",panelOpen);
        PlayerInventory.instance.OpenPanel(panelOpen);

    }
}
