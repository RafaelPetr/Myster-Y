using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(SceneValues))]
public class SceneValuesEditor : Editor {
    private string sceneName;

    public override void OnInspectorGUI() {
        SceneValues sceneValues = (SceneValues)target;

        if (string.IsNullOrEmpty(sceneName)) {
            sceneName = "sc_";
        }

        if (string.IsNullOrEmpty(sceneValues.GetName())) {
            sceneName = EditorGUILayout.TextField("Scene Name:",sceneName);
            if (GUILayout.Button("Save Name")) {
                sceneValues.SetSceneName(sceneName);
                UpdateLists(sceneValues);
            }
        }
        else {
            EditorGUILayout.LabelField("Scene Name: " + sceneValues.GetName());
            EditorGUILayout.LabelField("Grid: ");
            sceneValues.SetGrid((GameObject)EditorGUILayout.ObjectField(sceneValues.GetGrid(), typeof(GameObject), false));
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            for (int i = 0; i < sceneValues.GetDistances().Count; i++) {
                EditorGUILayout.LabelField("Scene Name: " + sceneValues.GetDistancedScene(i));
                sceneValues.SetDistance(i, EditorGUILayout.IntField("Distance:",sceneValues.GetDistance(i)));
                EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            }

            if (GUILayout.Button("Update List")) {
                UpdateLists(sceneValues);
            }
        }
        
        EditorUtility.SetDirty(sceneValues);
    }

    private void UpdateLists(SceneValues sceneValues) {
        List<string> allScenes = new List<string>();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string currentSceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            allScenes.Add(currentSceneName);
        }
        sceneValues.SetLists(allScenes);
    }
}