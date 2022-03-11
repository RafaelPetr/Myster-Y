using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCDestination{
    [SerializeField]private string sceneKey;
    [SerializeField]private Vector3Int position;

    public string GetScene() {
        return sceneKey;
    }

    public Vector3Int GetPosition() {
        return position;
    }
}
