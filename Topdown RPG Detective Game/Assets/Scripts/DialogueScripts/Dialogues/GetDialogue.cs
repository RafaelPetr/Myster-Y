using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GetDialogue : MonoBehaviour {
    [System.NonSerialized]public PlayerInventory inventory;
    public void Awake()
    {
        inventory = GameObject.Find("Detective").GetComponent<PlayerInventory>();
    }
    public abstract Dialogue DefineDialogue();

}
