using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    private new BoxCollider2D collider;
    private new Rigidbody2D rigidbody;

    [SerializeField]private SceneValues currentSceneValues;
    [SerializeField]private SceneValues nextSceneValues;
    [SerializeField]private Entrance entrance;

    private void Awake() {
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.32f, 0.32f, 0);
        collider.isTrigger = true;

        rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    }

    private void Start() {
        OnEndLoad();
        SceneController.instance.EndLoadEvent.AddListener(OnEndLoad);
    }

    private void OnEndLoad() {
        if (currentSceneValues.GetName() == SceneController.instance.GetSceneName()) {
            collider.enabled = true;
        }
        else {
            collider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();

        if (tags != null) {
            if (tags.HasTag(Tag.Player)) {
                SceneController.instance.Load(this);
            }
        }
    }

    public SceneValues GetCurrentSceneValues() {
        return currentSceneValues;
    }

    public SceneValues GetNextSceneValues() {
        return nextSceneValues;
    }

    public Entrance GetEntrance() {
        return entrance;
    }

    //Function to extend exit horizontally and vertically?
}
