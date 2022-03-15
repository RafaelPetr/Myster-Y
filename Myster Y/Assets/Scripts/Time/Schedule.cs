using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Schedule", fileName = "scriptable_schedule_")]
public class Schedule : ScriptableObject {
    [SerializeField]private Destination[] destinantions = new Destination[24];

    public Destination GetDestination(int index) {
        return destinantions[index];
    }
}

[System.Serializable]
public class Destination {
    [SerializeField]private SceneData sceneData;
    [SerializeField]private Vector3Int position;

    public SceneData GetSceneData() {
        return sceneData;
    }

    public Vector3Int GetPosition() {
        return position;
    }
}