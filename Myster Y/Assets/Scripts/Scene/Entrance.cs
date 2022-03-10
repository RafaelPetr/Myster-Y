using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {
    private SceneController sceneController;
    [SerializeField]private float playerDirectionX;
    [SerializeField]private float playerDirectionY;

    private void Start() {
        sceneController = SceneController.instance;

        if (name == sceneController.GetEntranceKey()) {
            sceneController.SpawnPlayer(this);
        }
    }

    public float GetDirectionX() {
        return playerDirectionX;
    }

    public float GetDirectionY() {
        return playerDirectionY;
    }
}
