using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnalysisManager : MonoBehaviour {
    public static AnalysisManager instance;

    private PlayerController controller;
    private SpriteRenderer playerSprite;

    private bool active;
    private bool enableAnalysis;
    private bool activationTrigger;
    private bool deactivationTrigger;

    [SerializeField]private GameObject analysisUI;
    [SerializeField]private GameObject analysisHand;
    [SerializeField]private Image analysisItem;

    private Animator analysisHandAnimator;

    [SerializeField]private GameObject textBox;
    private Animator textBoxAnimator;

    [SerializeField]private GameObject analysisTextBox;
    [SerializeField]private TextMeshProUGUI analysisName;
    [SerializeField]private TextMeshProUGUI analysisDescription;

    private string activeWritingText;
    private TextMeshProUGUI activeWritingUI;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        textBoxAnimator = textBox.GetComponent<Animator>();
        analysisHandAnimator = analysisHand.GetComponent<Animator>();
    }

    private void Update() {
        if (activationTrigger) {
            activationTrigger = false;
            active = true;
            enableAnalysis = true;
        }
        else if (deactivationTrigger) {
            AnimatorStateInfo currentAnimation = analysisHandAnimator.GetCurrentAnimatorStateInfo(0);
            float animationTime = Mathf.Lerp(0, 1, 1 - currentAnimation.normalizedTime);

            if (animationTime <= 0.1f) {
                deactivationTrigger = false;
                active = false;
                playerSprite.enabled = true;
                controller.enabled = true;
                InventoryManager.instance.OpenLastPanelClosed();
            }
        }
        else if (active) {
            if (Input.GetButtonDown("Interact")) {
                if (enableAnalysis) {
                    WriteAnalysis();
                }
                else if (activeWritingText != null) {
                    FinishWrite();
                }
                else {
                    EndAnalysis();
                }
            }
            else if (Input.GetButtonDown("Cancel")) {
                EndAnalysis();
            }
        }
    }

    public void StartAnalysis(Item item) {
        controller = PlayerController.instance;
        playerSprite = controller.gameObject.GetComponent<SpriteRenderer>();
        
        playerSprite.enabled = false;
        controller.enabled = false;

        analysisHandAnimator.SetBool("Active",true);

        analysisName.text = LocalizationManager.instance.GetLocalizedValue(item.key, 0);
        analysisItem.sprite = item.analysisImage;

        activeWritingText = LocalizationManager.instance.GetLocalizedValue(item.key, 1);
        activeWritingUI = analysisDescription;
        activeWritingUI.text = "";
       
        activationTrigger = true;
    }

    private void WriteAnalysis() {
        enableAnalysis = false;

        analysisTextBox.SetActive(true);
        textBoxAnimator.SetBool("Active",true);

        StopAllCoroutines();
        StartCoroutine(Write());
    }

    private IEnumerator Write() {
        foreach (char letter in activeWritingText.ToCharArray()) {
			activeWritingUI.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
        FinishWrite();
    }

    private void FinishWrite() {
        StopAllCoroutines();
        activeWritingUI.text = activeWritingText;

		activeWritingText = null;
		activeWritingUI = null;
    }

    private void EndAnalysis() {
        analysisTextBox.SetActive(false);
        textBoxAnimator.SetBool("Active",false);
        analysisHandAnimator.SetBool("Active",false);
        deactivationTrigger = true;
    }
}