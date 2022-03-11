using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController instance;
    private static Entrance targetEntrance;
    private string sceneKey;

    [SerializeField]private Animator crossfade;
    private float transitionTime = 1f;

    public UnityEvent FinishLoad;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        /*targetEntrance.WarpPlayer();
        FinishLoad.Invoke();*/
    }

    IEnumerator LoadScene() {
        PlayerController.instance.SetInTransition(true);
        crossfade.SetTrigger("Start");

        if (LocalizationManager.instance.GetIsReady()) {
            yield return new WaitForSeconds(transitionTime);
        }
        else {
            while (!LocalizationManager.instance.GetIsReady()) {
                yield return null;
            }
        }
        SceneManager.LoadScene(sceneKey);
    }

    public void Load(Exit exit) {
        Debug.Log(exit.GetSceneKey());
        sceneKey = exit.GetSceneKey();
        targetEntrance = exit.GetEntrance();
        StartCoroutine(LoadScene());
    }

    public string GetSceneKey() {
        return sceneKey;
    }

}