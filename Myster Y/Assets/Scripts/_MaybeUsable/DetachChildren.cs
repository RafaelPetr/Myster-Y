using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachChildren : MonoBehaviour {
    void Awake()
    {
        transform.DetachChildren();
    }
}
