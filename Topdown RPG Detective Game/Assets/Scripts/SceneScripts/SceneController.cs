using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class SceneController : MonoBehaviour {
    public static string spawner;
    public Animator transition;
    public float transitionTime = 1;
    public PlayerController controller;
    public void Load(string scene, string spawn) {
        StartCoroutine(LoadLevel(scene, spawn));
    }

    IEnumerator LoadLevel(string scene, string spawn) {
        controller.inTransition = true;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);
        spawner = spawn;
    }
}
