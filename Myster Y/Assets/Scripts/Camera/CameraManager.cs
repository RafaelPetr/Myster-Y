using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class CameraManager : MonoBehaviour {
    public static CameraManager instance;

    private PixelPerfectCamera pixelPerfectCamera; 
    private int minPPU;
    private int maxPPU;
    private bool zoom;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        minPPU = pixelPerfectCamera.assetsPPU;
        maxPPU = minPPU*3;
    }

    private void FixedUpdate() {
        if (zoom && pixelPerfectCamera.assetsPPU < maxPPU) {
            pixelPerfectCamera.assetsPPU += 2;
        }
        else if (!zoom && pixelPerfectCamera.assetsPPU > minPPU) {
            pixelPerfectCamera.assetsPPU -= 2;
        }
    }

    public void SetZoom(bool value) {
        zoom = value; 
    }
}
