using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortObjects : MonoBehaviour {
    [SerializeField]
    private bool objectMovement = false;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        sortObject();
    }

    private void Update() {
        if (objectMovement) {
            sortObject();
        }
    }

    void sortObject() {
        var min = spriteRenderer.bounds.min.y;
        spriteRenderer.sortingOrder = (int)(min*-100);
    }

}
