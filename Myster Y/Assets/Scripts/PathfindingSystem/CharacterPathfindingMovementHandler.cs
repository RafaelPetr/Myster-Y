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

public class CharacterPathfindingMovementHandler : MonoBehaviour {

    private float speed = 3f;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    private bool stopMove = false;
    private bool blockedByPlayer = false;
    private bool stoppedForDialogue = false;

    private int blockedByPlayerCounter = 200;
    private int stoppedForDialogueCounter = 200;

    private BoxCollider2D collider;

    [SerializeField]private DialogueSource dialogueSource; 

    private void Start() {
        Transform bodyTransform = transform.Find("Body");
        collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {

        if (!stopMove) {
            HandleMovement();
        }

        UpdateStopCounters();
        
    }
    
    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > .05f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } 
            else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }

    private void UpdateStopCounters() {
        if (blockedByPlayer && !stopMove) {
            blockedByPlayerCounter--;
            if (blockedByPlayerCounter <= 0) {
                blockedByPlayer = false;
                speed = 2f;
                collider.enabled = false;
            }
        }
        else if (blockedByPlayerCounter < 200 && !blockedByPlayer) {
            blockedByPlayerCounter++;
            if (blockedByPlayerCounter >= 200) {
                speed = 1f;
                collider.enabled = true;
            }
        }

        if (!dialogueSource.inDialogue) {
            stopMove = false;
        }
        else {
            stopMove = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            blockedByPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            blockedByPlayer = false;
        }
    }
}