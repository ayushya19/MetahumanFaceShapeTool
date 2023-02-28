using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Unity.Collections;

[System.Serializable]
public class PhonemePreset:PropertyAttribute
{

    [SerializeField]
    public Dictionary<string, float> Expressions = new Dictionary<string, float>();

    public string Visemes;

    
}



public class SaveFaceShapeVisemes : MonoBehaviour
{
    [HideInInspector]
    public List<PhonemePreset> phonemePresets;
    
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public string phonemeName;
    PhonemePreset currentPhonemePreset=new PhonemePreset();
    public void SaveIntoJson()
    {
        AddPhonemePresets(phonemeName);
        string phonemePresetsToSave = JsonConvert.SerializeObject(currentPhonemePreset);
        Debug.Log(phonemePresetsToSave);    
        System.IO.File.WriteAllText(Application.persistentDataPath + "/"+phonemeName+".json", phonemePresetsToSave);
        Debug.Log(Application.persistentDataPath + "/" + phonemeName + ".json");
    }

    void AddPhonemePresets(string visemes)
    {
        currentPhonemePreset.Visemes = (string)visemes.Clone();
        currentPhonemePreset.Expressions = GetCurrentBlendShapes();
        phonemePresets.Add(currentPhonemePreset);

    }
    public Dictionary<string,float> GetCurrentBlendShapes()
    {
        Mesh blendShapeNameMesh = skinnedMeshRenderer.sharedMesh;
        int lengthOfBlendShapes = blendShapeNameMesh.blendShapeCount;
        Dictionary<string, float> blendShapeToReturn=new Dictionary<string, float>(); 

        for (int i= 0; i < lengthOfBlendShapes;i++ )
        {
            string s = blendShapeNameMesh.GetBlendShapeName(i);
            blendShapeToReturn.Add(s.Substring(12), skinnedMeshRenderer.GetBlendShapeWeight(i));
            
        }



        return blendShapeToReturn;

    }


}


