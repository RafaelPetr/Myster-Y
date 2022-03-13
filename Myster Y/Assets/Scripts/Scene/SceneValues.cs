using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "scriptable_scene_", menuName = "Scene Values")]
public class SceneValues : ScriptableObject {
    [SerializeField]new private string name;
    [SerializeField]private GameObject grid;

    [SerializeField]private List<string> distancedScenes;
    [SerializeField]private List<int> distances;

    public string GetName() {
        return name;
    }

    public void SetSceneName(string value) {
        name = value;
    }

    public GameObject GetGrid() {
        return grid;
    }

    public void SetGrid(GameObject obj) {
        grid = obj;
    }

    public List<string> GetDistancedScenes() {
        return distancedScenes;
    }

    public List<int> GetDistances() {
        return distances;
    }

    public void SetLists(List<string> allScenes) {
        distancedScenes = new List<string>();
        distances = new List<int>();

        for (int i = 0; i < allScenes.Count; i++) {
            distancedScenes.Add(allScenes[i]);
            distances.Add(0);
        }

    }

    public string GetDistancedScene(int index) {
        return distancedScenes[index];
    }

    public int GetDistance(int index) {
        return distances[index];
    }

    public void SetDistance(int index, int value) {
        distances[index] = value;
    }
}