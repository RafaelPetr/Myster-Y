using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(CustomScene))]
public class CustomSceneEditor : Editor {
    private string sceneName;

    public override void OnInspectorGUI() {
        CustomScene scene = (CustomScene)target;

        if (string.IsNullOrEmpty(sceneName)) {
            sceneName = "sc_";
        }

        if (string.IsNullOrEmpty(scene.GetSceneName())) {
            sceneName = EditorGUILayout.TextField("Scene Name:",sceneName);
            if (GUILayout.Button("Save Name")) {
                scene.SetSceneName(sceneName);
                scene.SetDistancesList();
                scene.UpdateDistancesList(SceneManager.sceneCountInBuildSettings);
            }
        }
        else {
            EditorGUILayout.LabelField("Scene Name: " + scene.GetSceneName());
            EditorGUILayout.LabelField("Grid: ");
            scene.SetGrid((GameObject)EditorGUILayout.ObjectField(scene.GetGrid(), typeof(GameObject), false));
            EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);

            for (int i = 0; i < scene.GetDistancesList().Count; i++) {
                EditorGUILayout.LabelField("Scene Name: " + Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
                scene.SetDistance(i, EditorGUILayout.IntField("Distance:",scene.GetDistance(i)));
                EditorGUILayout.LabelField("",GUI.skin.horizontalSlider);
            }

            if (GUILayout.Button("Update List")) {
                scene.UpdateDistancesList(SceneManager.sceneCountInBuildSettings);
            }
        }
        
        EditorUtility.SetDirty(scene);
    } 
}