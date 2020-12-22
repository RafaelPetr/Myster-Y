using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class SceneController : MonoBehaviour {
    public static SceneController instance;

    public static string spawner;
    public Animator transition;
    public float transitionTime = 1;
    public PlayerController controller;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void Load(string scene, string spawn) {
        StartCoroutine(LoadLevel(scene, spawn));
    }

    IEnumerator LoadLevel(string scene, string spawn) {
        controller.inTransition = true;
        transition.SetTrigger("Start");

        if (LocalizationManager.instance.GetIsReady()) {
            yield return new WaitForSeconds(transitionTime);
        }

        else {
            while (!LocalizationManager.instance.GetIsReady()) {
                yield return null;
            }
        }

        SceneManager.LoadScene(scene);
        spawner = spawn;
    }
}
