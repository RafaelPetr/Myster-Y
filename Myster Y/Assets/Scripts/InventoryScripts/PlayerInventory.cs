using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerInventory : MonoBehaviour {
    public static bool key = false;
    public Animator animator;
    private bool openInventory;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetButtonDown("OpenInventory")) {
            openInventory = !openInventory;
            if (openInventory) {
                animator.SetTrigger("OpenInventory");
                CameraHandler.instance.ZoomIn();
            }
            else {
                animator.SetTrigger("CloseInventory");
                CameraHandler.instance.ZoomOut();
            }
        }
    }
}
