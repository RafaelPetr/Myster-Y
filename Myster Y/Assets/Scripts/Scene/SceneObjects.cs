using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjects : MonoBehaviour {
    public static SceneObjects instance;

    [SerializeField]private GameObject exitsParent;
    private List<Exit> exits = new List<Exit>();

    private void Awake() {
        exits = new List<Exit>(exitsParent.GetComponentsInChildren<Exit>());

        if (instance == null) {
            instance = this;

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    //public Get
}
