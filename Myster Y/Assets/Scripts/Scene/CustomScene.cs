using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "scriptable_scene_", menuName = "Custom Scene")]
public class CustomScene : ScriptableObject {
    [SerializeField]private string sceneName;
    [SerializeField]private GameObject grid;

    [SerializeField]private List<int> distances;

    public string GetSceneName() {
        return sceneName;
    }

    public void SetSceneName(string value) {
        sceneName = value;
    }

    public GameObject GetGrid() {
        return grid;
    }

    public void SetGrid(GameObject obj) {
        grid = obj;
    }

    public int GetDistance(int index) {
        return distances[index];
    }

    public void SetDistance(int index, int value) {
        distances[index] = value;
    }

    public List<int> GetDistancesList() {
        return distances;
    }

    public void SetDistancesList() {
        distances = new List<int>();
    }

    public void UpdateDistancesList(int scenesCount) {
        int listCount = scenesCount - distances.Count;

        if (listCount > 0) {
            for (int i = 0; i < listCount; i++) {
                distances.Add(0);
            }
        }
        else {
            for (int i = 0; i < -listCount; i++) {
                distances.RemoveAt(distances.Count - 1);
            }
        }
        
    }
}