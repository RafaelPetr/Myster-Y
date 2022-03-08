using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortable : MonoBehaviour {
    private bool movement;
    private int priority;
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

    private void Sort() {
        float minBound = render.bounds.min.y;
        render.sortingOrder = (int)(minBound*(-100)) + priority;
    }

    public void SetMovement(bool value) {
        movement = value;
    }

    public void SetPriority(int value) {
        priority = value;
    }
}
