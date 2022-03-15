using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(SceneData))]
public class SceneDataEditor : Editor {
    private string sceneName;

    public override void OnInspectorGUI() {
        SceneData sceneData = (SceneData)target;

        if (string.IsNullOrEmpty(sceneName)) {
            sceneName = "sc_";
        }

        if (string.IsNullOrEmpty(sceneData.GetSceneName())) {
            sceneName = EditorGUILayout.TextField("Scene Name:",sceneName);
            if (GUILayout.Button("Save Name")) {
                sceneData.SetSceneName(sceneName);
                UpdateLists(sceneData);
            }
        }
        else {
            EditorGUILayout.LabelField("Scene Name: " + sceneData.GetSceneName());
            EditorGUILayout.LabelField("Grid: ");
            sceneData.SetGrid((GameObject)EditorGUILayout.ObjectField(sceneData.GetGrid(), typeof(GameObject), false));
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            
            List<SceneDistance> sceneDistances = sceneData.GetDistances();

            for (int i = 0; i < sceneDistances.Count; i++) {
                if (sceneDistances[i].GetDistancedSceneName() != sceneData.GetSceneName()) {
                    EditorGUILayout.LabelField("Scene Name: " + sceneDistances[i].GetDistancedSceneName());
                    sceneDistances[i].SetSceneDistance(EditorGUILayout.IntField("Distance:",sceneDistances[i].GetSceneDistance()));
                    EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
                }
            }

            if (GUILayout.Button("Update List")) {
                UpdateLists(sceneData);
            }
        }
        
        EditorUtility.SetDirty(sceneData);
    }

    private void UpdateLists(SceneData sceneData) {
        List<string> allScenes = new List<string>();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string currentSceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            allScenes.Add(currentSceneName);
        }
        sceneData.UpdateSceneDistances(allScenes);
    }
}