using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingObject : MonoBehaviour {
    private bool inScene;
    [SerializeField]private SceneValues currentSceneValues;

    [System.NonSerialized]public float moveSpeed = 1.5f;
    private bool moving;
    private bool inPause;

    [SerializeField]private Schedule schedule;

    [SerializeField]private PathfindingGrid grid;
    private Pathfinding pathfinding;
    private List<PathNode> path = new List<PathNode>();
    private bool enablePathDebug = true;
    private Exit targetExit;
    private int targetTile;

    private TimeManager timeManager;
    private SceneController sceneController;

    private void Start() {
        sceneController = SceneController.instance;
        grid = grid.Generate();
        timeManager = TimeManager.instance;
        pathfinding = new Pathfinding(grid);

        timeManager.pauseTimeEvent += SetInPause;
        timeManager.UpdateRoutinesEvent.AddListener(ChangeDestination);
        sceneController.EndLoadEvent.AddListener(ControlLoading);

        ControlLoading();
    }

    private void Update() {
        if (path != null && enablePathDebug && inScene) {
            for (int i = 0; i < path.Count -1; i++) {
                Debug.DrawLine(path[i].GetWorldPosition(), path[i+1].GetWorldPosition(), Color.green);
            }
        }
    }

    public virtual void FixedUpdate() {
        ControlMovement();
    }

    private void ControlMovement() {
        if (moving && !BlockMovement()) {
            if (Vector3.Distance(transform.position, path[targetTile].GetWorldPosition()) > .01f) {
                transform.position = Vector3.MoveTowards(transform.position, path[targetTile].GetWorldPosition(), Time.deltaTime * moveSpeed);
            }
            else {
                if (targetTile + 1 < path.Count) {
                    targetTile++;
                    return;
                }
                moving = false;
                if (targetExit != null) {
                    TransitionToScene(targetExit.GetNextSceneValues());
                    targetExit = null;
                }
            }
        }
    }

    public virtual bool BlockMovement() {
        return inPause;
    }

    private void SetInPause(bool value) {
        inPause = value;
    }

    private void ChangeDestination() {
        int index = timeManager.GetNormalizedHour();

        UpdatePath(schedule.GetDestination(index));
    }

    private void UpdatePath(Destination destination) {
        moving = true;
        targetTile = 0;

        Vector3Int startGridCellPosition = grid.GetWorldPositionGrid(transform.position);

        Vector3Int startCell = grid.GetCellPositionTileset(startGridCellPosition);

        Vector3Int endCell = new Vector3Int();
        if (currentSceneValues == destination.GetScene()) {
            endCell = grid.GetCellPositionTileset(destination.GetPosition());
        }
        else {
            targetExit = SceneObjects.instance.GetPathExit(this, destination.GetScene());

            Vector3Int endGridCellPosition = grid.GetWorldPositionGrid(targetExit.transform.position);
            endCell = grid.GetCellPositionTileset(endGridCellPosition);
        }
        path = pathfinding.FindPath(startCell.x, startCell.y, endCell.x, endCell.y);
    }

    private void ControlLoading() {
        if (currentSceneValues.GetName() == sceneController.GetSceneName()) {
            Load();
        }
        else {
            Unload();
        }
    }

    private void Load() {
        inScene = true;

        Behaviour[] components = GetComponents<Behaviour>();
        foreach (Behaviour component in components) {
            component.enabled = true;
        }

        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Unload() {
        inScene = false;

        Behaviour[] components = GetComponents<Behaviour>();
        foreach (Behaviour component in components) {
            component.enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = false;

        this.enabled = true;
    }

    private void TransitionToScene(SceneValues sceneValues) {
        currentSceneValues = sceneValues;
        grid = sceneValues.GetGrid().GetComponent<PathfindingGrid>().Generate();
        pathfinding = new Pathfinding(grid);
        ControlLoading();
        ChangeDestination();
    }

    public SceneValues GetCurrentSceneValues() {
        return currentSceneValues;
    }
}