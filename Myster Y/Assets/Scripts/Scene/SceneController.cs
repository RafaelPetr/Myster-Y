using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController instance;

    private string sceneKey;
    private Entrance targetEntrance;

    [SerializeField]private Animator crossfade;
    private float transitionTime = 1f;

    public UnityEvent FinishLoadEvent;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        FinishLoadEvent = new UnityEvent();

        sceneKey = SceneManager.GetActiveScene().name;
        SceneManager.activeSceneChanged += FinishLoad;
    }

    private void FinishLoad(Scene current, Scene next) {
        sceneKey = next.name;

        targetEntrance.WarpPlayer();
        targetEntrance = null;

        FinishLoadEvent.Invoke();
    }

    private IEnumerator LoadScene(string scene) {
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
        
        SceneManager.LoadScene(scene);
    }

    public void Load(Exit exit) {
        targetEntrance = exit.GetEntrance();
        StartCoroutine(LoadScene(exit.GetSceneKey()));
    }

    public string GetSceneKey() {
        return sceneKey;
    }

}