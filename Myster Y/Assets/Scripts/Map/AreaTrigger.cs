using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour {
    [SerializeField]private GameObject area;

    private void Start() {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.25f, 0.25f, 0);
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();

        if (tags != null) {
            AreaManager.instance.LoadArea(area);
        }
    }
}