using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour {
    private new BoxCollider2D collider;

    private List<PathNode> path = new List<PathNode>();

    private Pathfinding pathfinding;
    private PathfindingGrid grid;

    private bool move;
    private bool blocked;
    private bool enableDebug = false;
    private int targetTile;

    private void Awake() {
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.32f, 0.32f, 0);
        gameObject.layer = LayerMask.NameToLayer("Collidable");
    }

    private void Start() {
        pathfinding = new Pathfinding();
        grid = pathfinding.GetGrid();
    }

    public void UpdatePath(Vector3Int endGridCellPosition) {
        targetTile = 0;

        Vector3Int startGridCellPosition = grid.GetWorldPositionGrid(transform.position);
        
        Vector3Int startCell = grid.GetCellPositionTileset(startGridCellPosition);
        Vector3Int endCell = grid.GetCellPositionTileset(endGridCellPosition);

        path = pathfinding.FindPath(startCell.x, startCell.y, endCell.x, endCell.y);
        move = true;
    }

    private void Update() {
        if (path != null && enableDebug) {
            for (int i = 0; i < path.Count -1; i++) {
                Debug.DrawLine(path[i].GetWorldPosition(), path[i+1].GetWorldPosition(), Color.green);
            }
        }
    }

    private void FixedUpdate() {
        ControlMovement();
    }

    private void ControlMovement() {
        if (move && !BlockMovement()) {
            if (Vector3.Distance(transform.position, path[targetTile].GetWorldPosition()) > .01f) {
                transform.position = Vector3.MoveTowards(transform.position, path[targetTile].GetWorldPosition(), Time.deltaTime * 1f);
            }
            else {
                if (targetTile + 1 < path.Count) {
                    targetTile++;
                    return;
                }
                move = false;
            }
        }
    }

    private bool BlockMovement() {
        return blocked;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            blocked = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            blocked = false;
        }
    }

}
