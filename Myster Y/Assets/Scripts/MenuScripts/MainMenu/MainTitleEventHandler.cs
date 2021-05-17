using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainTitleEventHandler : MonoBehaviour {
    public static MainTitleEventHandler instance;

    public UnityEvent getPen;
    public UnityEvent openNotebook;
    public UnityEvent displayMain;

    public Animator pen;
    public Animator notebook;

    public GameObject introMenu;
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public GameObject light;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    
    void Start() {
        getPen.AddListener(GetPen);
        openNotebook.AddListener(OpenNotebook);
        displayMain.AddListener(DisplayMain);
    }

    private void GetPen() {
        introMenu.SetActive(false);
        light.SetActive(true);
        pen.SetTrigger("Start");
    }

    private void OpenNotebook() {
        notebook.SetTrigger("Start");
    }

    private void DisplayMain() {
        mainMenu.SetActive(true);
        mainMenu.GetComponentsInChildren<Button>()[0].Select();
    }
}
