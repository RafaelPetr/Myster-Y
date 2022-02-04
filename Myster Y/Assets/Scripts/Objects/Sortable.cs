using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortable : MonoBehaviour {
    [SerializeField]private bool movement = false;
    private Renderer render;

    private void Awake() {
        render = GetComponent<Renderer>();
    }

    private void Start() {
        Sort();
    }

    private void Update() {
        if (movement) {
            Sort();
        }
    }

    void Sort() {
        float minBound = render.bounds.min.y;
        render.sortingOrder = (int)(minBound*(-100));
    }
}
