using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour {
    public string sceneName;
    public string spawnPointName;
    private SceneController sceneController;
    // Start is called before the first frame update
    void Start() {
        sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            sceneController.Load(sceneName,spawnPointName);
        }
    }
}
