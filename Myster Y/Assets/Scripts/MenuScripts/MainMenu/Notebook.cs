using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour {
    MainTitleEventHandler eventHandler;

    private void Start() {
        eventHandler = MainTitleEventHandler.instance;
    }

    private void Finish() {
        eventHandler.displayMain.Invoke();
    }
}
