using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    [SerializeField]private CustomScene scene;
    [SerializeField]private Entrance entrance;

    private void Awake() {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.32f, 0.32f, 0);
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

        //nextSceneGrid.GetComponent<PathfindingGrid>().Test();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();

        if (tags != null) {
            if (tags.HasTag(Tag.Player)) {
                SceneController.instance.Load(this);
            }
        }
    }

    public CustomScene GetScene() {
        return scene;
    }

    public Entrance GetEntrance() {
        return entrance;
    }
}
