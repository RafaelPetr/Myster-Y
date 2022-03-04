using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour {
    [SerializeField]private Vector3Int startWorldGridPosition;
    [SerializeField]private Vector3Int endWorldGridPosition;

    private List<PathNode> path = new List<PathNode>();

    private Pathfinding pathfinding;

    private void Start() {
        pathfinding = new Pathfinding();

        Vector3Int startCell = pathfinding.GetGrid().GetLocalCellPosition(startWorldGridPosition);
        Vector3Int endCell = pathfinding.GetGrid().GetLocalCellPosition(endWorldGridPosition);

        path = pathfinding.FindPath(startCell.x, startCell.y, endCell.x, endCell.y);
    }

    private void Update() {
        if (path != null) {
            for (int i = 0; i < path.Count -1; i++) {
                Debug.DrawLine(path[i].GetWorldPosition(), path[i+1].GetWorldPosition(), Color.green);
            }
        }
    }


}
