using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortObjects : MonoBehaviour {
    
    [SerializeField]private bool objectMovement = false;
    private Renderer objectRenderer;

    private void Awake() {
        objectRenderer = GetComponent<Renderer>();
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
        var min = objectRenderer.bounds.min.y;
        objectRenderer.sortingOrder = (int)(min*-100);
    }

}
