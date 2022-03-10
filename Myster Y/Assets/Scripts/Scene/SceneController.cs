using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController instance;
    private static string entranceKey;

    [SerializeField]private Animator crossfade;
    private float transitionTime = 1f;

    private void Awake() {
        instance = this;
    }

    IEnumerator LoadScene(string scene) {
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
        entranceKey = exit.GetEntranceKey();
        StartCoroutine(LoadScene(exit.GetSceneKey()));
    }

    public string GetEntranceKey() {
        return entranceKey;
    }

    public void SpawnPlayer(Entrance entrance) {
        PlayerController controller = PlayerController.instance;

        controller.SetDirectionX(entrance.GetDirectionX());
        controller.SetDirectionY(entrance.GetDirectionY());

        controller.transform.position = entrance.transform.position;
        controller.movePointer.position = entrance.transform.position;
        controller.interactPointer.transform.position = entrance.transform.position;
    }

}
