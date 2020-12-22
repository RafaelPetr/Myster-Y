using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour {
    public string sceneName;
    public string spawnPointName;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SceneController.instance.Load(sceneName,spawnPointName);
        }
    }
}
