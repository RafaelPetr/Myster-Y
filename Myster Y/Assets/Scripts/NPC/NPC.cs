using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : PathfindingObject {
    
    private new BoxCollider2D collider;
    private bool inCollision;
    private int collisionCounter;
    private bool cancelCollision;

    private void Awake() {
        gameObject.AddComponent<NPCAnimator>();

        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.32f, 0.32f, 0);
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

        gameObject.layer = LayerMask.NameToLayer("Collidable");

        Sortable sortable = gameObject.AddComponent<Sortable>();
        sortable.SetMovement(true);
    }

    public override void FixedUpdate() {
        ControlCollision();
        base.FixedUpdate();
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

    public override bool BlockMovement() {
        return base.BlockMovement() || inCollision;
    }

    #region Collision

    private void OnTriggerEnter2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();

        if (tags != null) {
            if (tags.HasTag(Tag.Player)) {
                inCollision = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();

        if (tags != null) {
            if (tags.HasTag(Tag.Player)) {
                inCollision = false;
            }
        }
    }

    #endregion
}