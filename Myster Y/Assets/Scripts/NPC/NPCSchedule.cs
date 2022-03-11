using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSchedule : MonoBehaviour {
    private TimeManager timeManager;

    private NPC npc;
    [SerializeField]private NPCDestination[] destinantions = new NPCDestination[24];

    private void Start() {
        timeManager = TimeManager.instance;
        timeManager.UpdateRoutinesEvent.AddListener(ChangeDestination);
        
        npc = GetComponent<NPC>();
    }

    private void ChangeDestination() {
        int index = timeManager.GetNormalizedHour();

        npc.UpdatePath(destinantions[index]);
    }
}
