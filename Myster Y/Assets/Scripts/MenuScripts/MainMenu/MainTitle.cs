﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainTitle : ButtonMenu {
    MainTitleEventHandler eventHandler;

    private void Start() {
        eventHandler = MainTitleEventHandler.instance;
    }

    public void IntroMenu() {
        eventHandler.getPen.Invoke();
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("To dando o foradaquitaligadomeuirmao");
        Application.Quit();
    }
}
