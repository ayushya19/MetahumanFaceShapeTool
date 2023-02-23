using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveFaceShapeVisemes))]
public class FaceShapeToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveFaceShapeVisemes saveFaceShapeVisemes = (SaveFaceShapeVisemes)target;
        GUIStyle myBoldLabel = new GUIStyle(EditorStyles.label);

        if (GUILayout.Button("Save Value"))
        {
            saveFaceShapeVisemes.SaveIntoJson();
        }
        //foreach (PhonemePreset pP in saveFaceShapeVisemes.phonemePresets)
        //{ GUILayout.BeginHorizontal();
        //    GUILayout.Label(pP.Visemes, myBoldLabel); }

    }


}