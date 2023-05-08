using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveFaceShapeVisemes))]
public class FaceShapeToolEditor : Editor
{
    Rect Position = Rect.zero;
    SerializedProperty sp;
    GUIContent label = new GUIContent();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //SaveFaceShapeVisemes saveFaceShapeVisemes = (SaveFaceShapeVisemes)target;


        if (GUILayout.Button("Save Value"))
        {
            //saveFaceShapeVisemes.SaveIntoJson();
        }

        //GUI.enabled = false;
        //EditorGUI.PropertyField(Rect.zero, sp, label);
        //GUI.enabled = true;
    }

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    GUI.enabled = false;
    //    EditorGUI.PropertyField(position, property, label);
    //    GUI.enabled = true;
    //}


}
