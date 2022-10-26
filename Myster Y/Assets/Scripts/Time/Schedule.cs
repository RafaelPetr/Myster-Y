using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Schedule", fileName = "scriptable_schedule_")]
public class Schedule : ScriptableObject {
    [SerializeField]private Destination[] destinantions = new Destination[24];

    #region Getters

        public Destination GetDestination(int index) {
            return destinantions[index];
        }

    #endregion
}

[System.Serializable]
public class Destination {
    [SerializeField]private SceneData sceneData;
    [SerializeField]private Vector3Int position;

    #region Getters

        public SceneData GetSceneData() {
            return sceneData;
        }

        public Vector3Int GetPosition() {
            return position;
        }

    #endregion
}