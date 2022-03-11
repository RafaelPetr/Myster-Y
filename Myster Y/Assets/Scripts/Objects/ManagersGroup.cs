using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersGroup : MonoBehaviour {
    private static bool active;

    private void Awake() {
        if (!active) {
            active = true;

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
