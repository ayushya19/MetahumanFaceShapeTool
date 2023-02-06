using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
[Serializable]
public class viseme:MonoBehaviour
{
    
    //SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField]
    public string phonemes;
    [SerializeField]
    public Dictionary<string, float> facialShape=new Dictionary<string, float>();

    
    


    public void GetBlendShapeWeight(viseme viseme)
    {
        SkinnedMeshRenderer skinnedMeshRenderer=GetComponent<SkinnedMeshRenderer>();
        
        Mesh m = (Mesh)skinnedMeshRenderer.sharedMesh;
        for(int i = 0; i < m.blendShapeCount; i++)
        {
            Debug.Log(m.GetBlendShapeName(i));
            Debug.Log(skinnedMeshRenderer.GetBlendShapeWeight(i));


           facialShape.Add(m.GetBlendShapeName(i), skinnedMeshRenderer.GetBlendShapeWeight(i));

        }
    }

    
}

[CustomEditor(typeof(viseme))]
public class SaveBlendShapeValues : Editor
{
    public override void OnInspectorGUI()
    {
       
        viseme vismes = (viseme)target;

        //vismes.experience = EditorGUILayout.IntField("Experience", vismes.experience);
        vismes.phonemes=EditorGUILayout.TextField("Phonemes", vismes.phonemes);
        if (GUILayout.Button("Save Visemes"))
        {
            
            vismes.GetBlendShapeWeight(vismes);
            
            string json = JsonConvert.SerializeObject(vismes.facialShape);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/"+ vismes.phonemes+".json", json);

        }
    }
}
