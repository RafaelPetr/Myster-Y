using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGroup : MonoBehaviour {
    private static NPCGroup instance;

    private NPC[] npcs;

    private void Awake() {
        if (instance == null) {
            instance = this;
            
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        npcs = GetComponentsInChildren<NPC>();
    }

    public void LoadNPC() {
        foreach (NPC npc in npcs) {
            
        }
    }
}
