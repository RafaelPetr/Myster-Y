using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Exit : MonoBehaviour {
    [SerializeField]private string sceneKey;
    [SerializeField]private Entrance entrance;

    [SerializeField]private GameObject nextSceneGrid;

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

    public string GetSceneKey() {
        return sceneKey;
    }

    public Entrance GetEntrance() {
        return entrance;
    }
}
