using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneDistance {
    [SerializeField]private string sceneName;
    [SerializeField]private int distance;

    public SceneDistance(string sceneName, int distance) {
        this.sceneName = sceneName;
        this.distance = distance;
    }

    public string GetSceneName() {
        return sceneName;
    }

    public void SetSceneName(string value) {
        sceneName = value;
    }

    public int GetSceneDistance() {
        return distance;
    }

    public void SetSceneDistance(int value) {
        distance = value;
    }
}
