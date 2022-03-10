using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    [SerializeField]private string sceneKey;
    [SerializeField]private string entranceKey;

    private void Awake() {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.32f, 0.32f, 0);
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SceneController.instance.Load(this);
        }
    }

    public string GetSceneKey() {
        return sceneKey;
    }

    public string GetEntranceKey() {
        return entranceKey;
    }
}
