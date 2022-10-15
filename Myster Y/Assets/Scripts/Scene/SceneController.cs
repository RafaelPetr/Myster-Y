using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController instance;

    private string sceneName;
    private Entrance targetEntrance;

    [SerializeField]private Animator crossfade;
    private float transitionTime = 1f;

    [System.NonSerialized]public UnityEvent StartLoadEvent;
    [System.NonSerialized]public UnityEvent EndLoadEvent;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        StartLoadEvent = new UnityEvent();
        EndLoadEvent = new UnityEvent();

        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Start() {
        SceneManager.activeSceneChanged += FinishLoad;
    }

    private void FinishLoad(Scene current, Scene next) {
        sceneName = next.name;

        targetEntrance.WarpPlayer();
        targetEntrance = null;

        EndLoadEvent.Invoke();
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
        StartLoadEvent.Invoke();
        targetEntrance = exit.GetEntrance();
        StartCoroutine(LoadScene(exit.GetNextSceneData().GetSceneName()));
    }

    public string GetSceneName() {
        return sceneName;
    }
}