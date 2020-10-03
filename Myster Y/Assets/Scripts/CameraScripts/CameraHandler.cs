using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class CameraHandler : MonoBehaviour {
    public static CameraHandler instance;
    public int minPPU;
    public int maxPPU;
    private PixelPerfectCamera pixelPerfectCamera; 
    private bool zoom;

    private void Start() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    private void FixedUpdate() {
        if (zoom && pixelPerfectCamera.assetsPPU < maxPPU) {
            pixelPerfectCamera.assetsPPU += 3;
        }
        else if (!zoom && pixelPerfectCamera.assetsPPU > minPPU) {
            pixelPerfectCamera.assetsPPU -= 3;
        }
    }

    public void ZoomIn() {
        zoom = true;
    }

    public void ZoomOut() {
        zoom = false;
    }
}
