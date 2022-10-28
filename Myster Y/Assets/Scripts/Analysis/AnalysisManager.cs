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
    private Queue<string> textQueue = new Queue<string>();

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

        controller = PlayerController.instance;
        playerSprite = controller.gameObject.GetComponent<SpriteRenderer>();
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
                    ExecuteNextText();
                }
            }
            else if (Input.GetButtonDown("Cancel")) {
                EndAnalysis();
            }
        }
    }

    public void StartAnalysis(Item item) {
        playerSprite.enabled = false;
        controller.enabled = false;

        analysisHandAnimator.SetBool("Active",true);

        LocalizationItem localizedItem = LocalizationManager.GetLocalizedItem(item.GetKey());

        analysisName.text = localizedItem.GetName();
        analysisItem.sprite = item.GetAnalysisImage();

        activeWritingText = localizedItem.GetDescription(0);
        analysisDescription.text = "";

        for (int i = 1; i < localizedItem.GetDescription().Count; i++) {
            textQueue.Enqueue(localizedItem.GetDescription(i));
        }
    
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
			analysisDescription.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
        FinishWrite();
    }

    private void FinishWrite() {
        StopAllCoroutines();
        analysisDescription.text = activeWritingText;

		activeWritingText = null;
    }

    private void ExecuteNextText() {
        if (textQueue.Count == 0) {
            EndAnalysis();
			return;
		}
        analysisDescription.text = "";

        activeWritingText = textQueue.Dequeue();
        StartCoroutine(Write());
    }

    private void EndAnalysis() {
        StopAllCoroutines();

        analysisTextBox.SetActive(false);
        textBoxAnimator.SetBool("Active",false);
        analysisHandAnimator.SetBool("Active",false);
        deactivationTrigger = true;
    }
}