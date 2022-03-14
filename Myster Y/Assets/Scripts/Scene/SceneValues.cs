using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "scriptable_scene_", menuName = "Scene Values")]
public class SceneValues : ScriptableObject {
    [SerializeField]new private string name;
    [SerializeField]private GameObject grid;
    [SerializeField]private List<SceneDistance> sceneDistances = new List<SceneDistance>();

    public string GetName() {
        return name;
    }

    public void SetName(string value) {
        name = value;
    }

    public GameObject GetGrid() {
        return grid;
    }

    public void SetGrid(GameObject obj) {
        grid = obj;
    }

    public List<SceneDistance> GetDistances() {
        return sceneDistances;
    }

    public void UpdateSceneDistances(List<string> allScenes) {
        foreach (string sceneName in allScenes) {
            SceneDistance foundSceneDistance = sceneDistances.Find(sceneDistance => sceneDistance.GetSceneName() == sceneName);
            if (foundSceneDistance == null) {
                sceneDistances.Add(new SceneDistance(sceneName, 0));
            }
        }

        for (int i = 0; i < sceneDistances.Count; i++) {
            string foundScene = allScenes.Find(sceneName => sceneDistances[i].GetSceneName() == sceneName);
            if (foundScene == null) {
                sceneDistances.Remove(sceneDistances[i]);
                i--;
            }
        }

        sceneDistances.Sort((x, y) => string.Compare(x.GetSceneName(), y.GetSceneName()));
    }
}