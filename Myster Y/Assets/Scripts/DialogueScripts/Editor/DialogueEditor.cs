using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI(); 

        Dialogue dialogue = (Dialogue)target;

        if (GUILayout.Button("Create Sentence")) {
            dialogue.NewSentence();
        }
    }
    
}