using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour {
    private SceneController sceneController;

    private new BoxCollider2D collider;

    private bool inScene;
    private string currentScene;

    private float moveSpeed = 1.5f;
    private bool walking;
    private bool inPause;
    private bool inCollision;

    private int collisionCounter;
    private bool cancelCollision;

    private bool enablePathDebug = true;
    private int targetTile;
    private List<PathNode> path = new List<PathNode>();
    private Pathfinding pathfinding;
    private PathfindingGrid grid;

    private void Awake() {
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.32f, 0.32f, 0);
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

        gameObject.layer = LayerMask.NameToLayer("Collidable");

        Sortable sortable = gameObject.AddComponent<Sortable>();
        sortable.SetMovement(true);

        sceneController = SceneController.instance;
    }

    private void Start() {
        pathfinding = new Pathfinding();
        grid = pathfinding.GetGrid();

        TimeManager.instance.pauseTimeEvent += SetInPause;

        if (currentScene == null) {
            currentScene = SceneController.instance.GetSceneKey();
            inScene = true;
        }
    }

    private void Update() {
        if (path != null && enablePathDebug && inScene) {
            for (int i = 0; i < path.Count -1; i++) {
                Debug.DrawLine(path[i].GetWorldPosition(), path[i+1].GetWorldPosition(), Color.green);
            }
        }
    }

    private void FixedUpdate() {
        if (inScene) {
            ControlCollision();
            ControlMovement();
        }
        else {
            ControlOutOfScene();
        }
    }

    private void ControlCollision() {
        if (inCollision) {
            collisionCounter++;
            if (collisionCounter >= 120) {
                collider.enabled = false;
                moveSpeed *= 2;
                cancelCollision = true;
            }
        }
        else if (cancelCollision) {
            collisionCounter--;
            if (collisionCounter <= 0) {
                collider.enabled = true;
                moveSpeed /= 2;
                cancelCollision = false;
            }
        }
    }

    private void ControlMovement() {
        if (walking && !BlockMovement()) {
            if (Vector3.Distance(transform.position, path[targetTile].GetWorldPosition()) > .01f) {
                transform.position = Vector3.MoveTowards(transform.position, path[targetTile].GetWorldPosition(), Time.deltaTime * moveSpeed);
            }
            else {
                if (targetTile + 1 < path.Count) {
                    targetTile++;
                    return;
                }
                walking = false;
            }
        }
    }

    private void ControlOutOfScene() {
        Debug.Log("Controla pae");
        if (currentScene == sceneController.GetSceneKey()) {
            EnterScene();
        }
    }
    
    private void EnterScene() {
        Behaviour[] components = GetComponents<Behaviour>();
        foreach (Behaviour component in components) {
            component.enabled = true;
        }
        GetComponent<SpriteRenderer>().enabled = true;

        inScene = true;
    }
    
    private void SetInPause(bool value) {
        inPause = value;
    }

    private bool BlockMovement() {
        return inCollision || inPause;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            inCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            inCollision = false;
        }
    }

    public void UpdatePath(Vector3Int endGridCellPosition) {
        targetTile = 0;

        Vector3Int startGridCellPosition = grid.GetWorldPositionGrid(transform.position);
        
        Vector3Int startCell = grid.GetCellPositionTileset(startGridCellPosition);
        Vector3Int endCell = grid.GetCellPositionTileset(endGridCellPosition);

        path = pathfinding.FindPath(startCell.x, startCell.y, endCell.x, endCell.y);
        walking = true;
    }

    public void ExitScene(string scene) {
        Behaviour[] components = GetComponents<Behaviour>();
        foreach (Behaviour component in components) {
            component.enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = false;

        this.enabled = true;
        GetComponent<CharacterSchedule>().enabled = true;

        inScene = false;
        currentScene = scene;
    }
}
