/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour {
    
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    [SerializeField] private CharacterRoutineManager routineManager;
    private Pathfinding pathfinding;
    private GameTimeManager timeManager;

    private void Awake() {
        timeManager = GameObject.Find("GameTimeManager").GetComponent<GameTimeManager>();
    }

    private void Start() {
        pathfinding = new Pathfinding(20, 10);
        timeManager.OnChangeHour.AddListener(SetNextDestination);
    }

    /*private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * .32f + Vector3.one * .16f, new Vector3(path[i+1].x, path[i+1].y) * .32f + Vector3.one * .16f, Color.green, 16f);
                }
            }
            characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }*/

    private void SetNextDestination() {
        pathfinding.GetGrid().GetXY(routineManager.currentDestination, out int x, out int y);
        List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
        if (path != null) {
            for (int i=0; i<path.Count - 1; i++) {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * .32f + Vector3.one * .16f, new Vector3(path[i+1].x, path[i+1].y) * .32f + Vector3.one * .16f, Color.green, 16f);
            }
        }
        characterPathfinding.SetTargetPosition(routineManager.currentDestination);
    }

}
