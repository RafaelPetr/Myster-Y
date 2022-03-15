using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjects : MonoBehaviour {
    public static SceneObjects instance;

    [SerializeField]private GameObject sceneExits;
    private List<Exit> exits = new List<Exit>();

    private void Awake() {
        exits = new List<Exit>(sceneExits.GetComponentsInChildren<Exit>());

        if (instance == null) {
            instance = this;

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public Exit GetPathExit(PathfindingObject pathfindingObject, SceneValues nextScene) {
        List<Exit> exitsInScene = exits.FindAll(exit => exit.GetCurrentSceneValues() == pathfindingObject.GetCurrentSceneValues());
        List<SceneDistance> genericSceneDistances = exitsInScene[0].GetNextSceneValues().GetDistances();

        SceneDistance nextSceneInDistances = genericSceneDistances.Find(scene => scene.GetSceneName() == nextScene.GetName());
        int nextSceneIndex = genericSceneDistances.IndexOf(nextSceneInDistances);
        
        List<int> distances = new List<int>();
        for (int i = 0; i < exitsInScene.Count; i++) {
            SceneValues nextSceneValues = exitsInScene[i].GetNextSceneValues();
            distances.Add(nextSceneValues.GetDistances()[i].GetSceneDistance());
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
    }
}
