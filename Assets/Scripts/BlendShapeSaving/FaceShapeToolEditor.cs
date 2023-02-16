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
        

        if(GUILayout.Button("Save Value"))
        {
            saveFaceShapeVisemes.SaveIntoJson();
        }
        
    }

   
}
