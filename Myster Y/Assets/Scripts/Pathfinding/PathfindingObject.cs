using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingObject : MonoBehaviour {
    private bool inScene;
    [SerializeField]private SceneData currentSceneData;

    [System.NonSerialized]public float moveSpeed = 1.5f;
    private bool moving;
    private bool inPause;

    [SerializeField]private Schedule schedule;

    private Pathfinding pathfinding;
    private List<PathNode> path = new List<PathNode>();

    private int targetTile;
    private Exit targetExit;

    private bool enablePathDebug = true;

    private TimeManager timeManager;
    private SceneController sceneController;

    private void Start() {
        sceneController = SceneController.instance;
        timeManager = TimeManager.instance;
        SetPathfinding();

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
                FinishMovement();
            }
        }
    }

    private void FinishMovement() {
        if (targetTile + 1 < path.Count) {
            targetTile++;
            return;
        }
        moving = false;

        if (targetExit != null) {
            TransitionToScene(targetExit);
        }
    }

    private void SetPathfinding() {
        PathfindingGrid pathfindingGrid = currentSceneData.GetGrid().GetComponent<PathfindingGrid>().Generate();
        pathfinding = new Pathfinding(pathfindingGrid);
    }

    public virtual bool BlockMovement() {
        return inPause;
    }

    private void SetInPause(bool value) {
        inPause = value;
    }

    private void ChangeDestination() {
        if (schedule != null) {
            int index = timeManager.GetNormalizedHour();

            UpdatePath(schedule.GetDestination(index));
        }
    }

    private void UpdatePath(Destination destination) {
        moving = true;
        targetTile = 0;

        PathfindingGrid pathfindingGrid = pathfinding.GetGrid();

        Vector3Int startGridCellPosition = pathfindingGrid.GetWorldPositionGrid(transform.position);
        Vector3Int startCell = pathfindingGrid.GetCellPositionTileset(startGridCellPosition);

        Vector3Int endCell = new Vector3Int();
        if (currentSceneData == destination.GetSceneData()) {
            targetExit = null;
            endCell = pathfindingGrid.GetCellPositionTileset(destination.GetPosition());
        }
        else {
            targetExit = SceneWarps.instance.GetOptimalExit(this, destination.GetSceneData());

            Vector3Int endGridCellPosition = pathfindingGrid.GetWorldPositionGrid(targetExit.transform.position);
            endCell = pathfindingGrid.GetCellPositionTileset(endGridCellPosition);
        }
        path = pathfinding.FindPath(startCell.x, startCell.y, endCell.x, endCell.y);
    }

    private void TransitionToScene(Exit exit) {
        SceneData sceneData = exit.GetNextSceneData();
        currentSceneData = sceneData;
        transform.position = exit.GetEntrance().transform.position;

        SetPathfinding();
        ControlLoading();
        ChangeDestination();

        targetExit = null;
    }

    private void ControlLoading() {
        if (currentSceneData.GetSceneName() == sceneController.GetSceneName()) {
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

    public SceneData GetCurrentSceneData() {
        return currentSceneData;
    }
}