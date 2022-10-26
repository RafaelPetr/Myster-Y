using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "scriptable_scenedata_", menuName = "Scene Data")]
public class SceneData : ScriptableObject {
    [SerializeField]new private string name;
    [SerializeField]private GameObject grid;
    [SerializeField]private List<SceneDistance> sceneDistances = new List<SceneDistance>();

    public void UpdateSceneDistances(List<string> allScenes) {
        foreach (string sceneName in allScenes) {
            SceneDistance foundSceneDistance = sceneDistances.Find(sceneDistance => sceneDistance.GetDistancedSceneName() == sceneName);
            if (foundSceneDistance == null) {
                sceneDistances.Add(new SceneDistance(sceneName, 0));
            }
        }

        for (int i = 0; i < sceneDistances.Count; i++) {
            string foundScene = allScenes.Find(sceneName => sceneDistances[i].GetDistancedSceneName() == sceneName);
            if (foundScene == null) {
                sceneDistances.Remove(sceneDistances[i]);
                i--;
            }
        }

        sceneDistances.Sort((x, y) => string.Compare(x.GetDistancedSceneName(), y.GetDistancedSceneName()));
    }

    #region Getters

        public string GetSceneName() {
            return name;
        }

        public GameObject GetGrid() {
            return grid;
        }

        public List<SceneDistance> GetDistances() {
            return sceneDistances;
        }

    #endregion

    #region Setters

        public void SetSceneName(string value) {
            name = value;
        }

        public void SetGrid(GameObject obj) {
            grid = obj;
        }

    #endregion
}

[System.Serializable]
public class SceneDistance {
    [SerializeField]private string distancedScene;
    [SerializeField]private int distance;

    public SceneDistance(string distancedScene, int distance) {
        this.distancedScene = distancedScene;
        this.distance = distance;
    }

    #region Getters

        public string GetDistancedSceneName() {
            return distancedScene;
        }

        public int GetSceneDistance() {
            return distance;
        }

    #endregion

    #region Setters

        public void SetDistancedSceneName(string value) {
            distancedScene = value;
        }

        public void SetSceneDistance(int value) {
            distance = value;
        }

    #endregion
}