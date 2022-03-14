using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGroup : MonoBehaviour {
    public static ExitGroup instance;

    private List<Exit> exits = new List<Exit>();

    private void Awake() {
        exits = new List<Exit>(GetComponentsInChildren<Exit>());

        if (instance == null) {
            instance = this;

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    /*public Exit GetPathExit(PathfindingObject pathfindingObject, SceneValues nextScene) {
        List<Exit> exitsInScene = exits.FindAll(exit => exit.GetCurrentSceneValues() == pathfindingObject.GetCurrentSceneValues());

        int nextSceneIndex = exitsInScene[0].GetNextSceneValues().GetDistances().IndexOf(nextScene.GetName());
        List<int> distances = new List<int>();

        for (int i = 0; i < exitsInScene.Count; i++) {
            SceneValues nextSceneValues = exitsInScene[i].GetNextSceneValues();
            distances.Add(nextSceneValues.GetDistance(nextSceneIndex));
        }

        int minDistance = distances.AsQueryable().Min();

        List<Exit> possibleExits = new List<Exit>();
        for (int i = 0; i < exitsInScene.Count; i++) {
            if (distances[i] == minDistance) {
                possibleExits.Add(exitsInScene[i]);
            }
        }
        
        Exit optimalExit = possibleExits[0];
        for (int i = 0; i < possibleExits.Count; i++) {
            float optimalExitDistance = Vector3.Distance(optimalExit.transform.position, pathfindingObject.transform.position);
            float currentExitDistance = Vector3.Distance(possibleExits[i].transform.position, pathfindingObject.transform.position);
            
            if (currentExitDistance < optimalExitDistance) {
                optimalExit = possibleExits[i];
            }
        }
        
        return optimalExit;
    }*/
}
