using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    [SerializeField]private string sceneKey;
    [SerializeField]private string entranceKey;

    private void Awake() {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.32f, 0.32f, 0);
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();

        if (tags != null) {
            if (tags.HasTag(Tag.Player)) {
                SceneController.instance.Load(this);
            }
            else if (tags.HasTag(Tag.Character)) {
                other.GetComponent<CharacterAI>().ExitScene(this.sceneKey);
            }
        }
    }

    public string GetSceneKey() {
        return sceneKey;
    }

    public string GetEntranceKey() {
        return entranceKey;
    }
}
