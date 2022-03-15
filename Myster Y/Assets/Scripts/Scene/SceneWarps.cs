using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWarps : MonoBehaviour {
    public static SceneWarps instance;

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

    public Exit GetOptimalExit(PathfindingObject pathfindingObject, SceneData nextSceneData) {
        List<Exit> exitsInScene = exits.FindAll(exit => exit.GetCurrentSceneData() == pathfindingObject.GetCurrentSceneData());
        List<SceneDistance> genericSceneDistances = exitsInScene[0].GetNextSceneData().GetDistances(); //Get a generic scene distance to find index of next scene

        SceneDistance nextSceneInDistances = genericSceneDistances.Find(distance => distance.GetDistancedSceneName() == nextSceneData.GetSceneName()); //Get next scene in distances list
        int nextSceneIndex = genericSceneDistances.IndexOf(nextSceneInDistances); //Get index of the next scene in distances list
        
        List<int> distances = new List<int>();
        for (int i = 0; i < exitsInScene.Count; i++) { //Create list with all distances of exits to next scene target
            SceneData nextSceneValues = exitsInScene[i].GetNextSceneData();
            distances.Add(nextSceneValues.GetDistances()[i].GetSceneDistance());
        }

        int minDistance = distances.AsQueryable().Min(); //Get the minimal distance to the next scene

        List<Exit> minimalExits = new List<Exit>();
        for (int i = 0; i < exitsInScene.Count; i++) { //Create list with all exits with the minimal distance
            if (distances[i] == minDistance) {
                minimalExits.Add(exitsInScene[i]);
            }
        }
        
        Exit optimalExit = minimalExits[0];
        for (int i = 0; i < minimalExits.Count; i++) { //Find the optimal exit based on distance to object if there is more than one minimal exit
            float optimalExitDistance = Vector3.Distance(optimalExit.transform.position, pathfindingObject.transform.position);
            float currentExitDistance = Vector3.Distance(minimalExits[i].transform.position, pathfindingObject.transform.position);
            
            if (currentExitDistance < optimalExitDistance) {
                optimalExit = minimalExits[i];
            }
        }
        
        return optimalExit;
    }
}
