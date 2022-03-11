using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {
    private SceneController sceneController;
    [SerializeField]private float playerDirectionX;
    [SerializeField]private float playerDirectionY;

    private void Start() {
        sceneController = SceneController.instance;
    }

    public void WarpPlayer() {
        PlayerController controller = PlayerController.instance;

        controller.SetDirectionX(playerDirectionX);
        controller.SetDirectionY(playerDirectionY);

        controller.transform.position = transform.position;
        controller.movePointer.position = transform.position;
        controller.interactPointer.transform.position = transform.position;
    }
}
