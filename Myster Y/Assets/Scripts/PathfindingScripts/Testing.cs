﻿/* 
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
    public Vector3[] unwalkableTiles;
    private GameTimeManager timeManager;

    private void Awake() {
        pathfinding = new Pathfinding(20, 10);
        foreach(Vector3 tile in unwalkableTiles) {
            pathfinding.GetGrid().GetXY(tile, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(false);
        }
    }

    private void Start() {
        timeManager = GameObject.Find("GameTimeManager").GetComponent<GameTimeManager>();
        timeManager.OnChangeHour.AddListener(SetNextDestination);
    }

    private void SetNextDestination() {
        pathfinding.GetGrid().GetXY(routineManager.currentDestination, out int x, out int y);
        List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
        if (path != null) {
            for (int i=0; i<path.Count - 1; i++) {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 1f + Vector3.one * .5f, new Vector3(path[i+1].x, path[i+1].y) * 1f + Vector3.one * .5f, Color.green, .5f);
            }
        }
        characterPathfinding.SetTargetPosition(routineManager.currentDestination);
    }

}