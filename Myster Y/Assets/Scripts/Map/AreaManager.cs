using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour {
    public static AreaManager instance;
    [SerializeField]private GameObject loadedArea;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        if (loadedArea != null) {
            loadedArea.SetActive(true);
        }
    }

    public void LoadArea(GameObject area) {
        loadedArea.SetActive(false);
        loadedArea = area;
        loadedArea.SetActive(true);
    }
}
