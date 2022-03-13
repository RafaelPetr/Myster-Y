using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Destination {
    [SerializeField]private SceneValues sceneValues;
    [SerializeField]private Vector3Int position;

    public SceneValues GetScene() {
        return sceneValues;
    }

    public Vector3Int GetPosition() {
        return position;
    }
}