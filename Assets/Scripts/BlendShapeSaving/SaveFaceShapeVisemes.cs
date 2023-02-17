using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class PhonemePreset
{

    [SerializeField]
    public Dictionary<string, float> Expressions = new Dictionary<string, float>();

    public string Visemes;
    //public PhonemePreset(Dictionary<string, float> expressions, string visemes)
    //{
    //    Expressions = expressions;
    //    Visemes = visemes;
    //}
    //{
    //        //count 49
    //        //blendshape count 50
    //    {"eyeBlink_L", 0},
    //    {"eyeBlink_R", 0},
    //    {"eyeLookDown_L", 0},
    //    {"eyeLookDown_R", 0},
    //    {"eyeLookIn_L", 0},
    //    {"eyeLookIn_R", 0},
    //    {"eyeLookOut_L", 0},
    //    {"eyeLookOut_R", 0},
    //    {"eyeLookUp_L", 0},
    //    {"eyeLookUp_R", 0},
    //    {"eyeSquint_L", 0},
    //    {"eyeSquint_R", 0},
    //    {"eyeWide_L", 0},
    //    {"eyeWide_R", 0},
    //    {"jawForward", 0},
    //    {"jawLeft", 0},
    //    {"jawRight", 0},
    //    {"jawOpen", 100},
    //    {"mouthClose", 0},
    //    {"mouthFunnel", 0},
    //    {"mouthPucker", 0},
    //    {"mouthLeft", 0},
    //    {"mouthRight", 0},
    //    {"mouthSmile_L", 0},
    //    {"mouthSmile_R", 0},
    //    {"mouthFrown_L", 0},
    //    {"mouthFrown_R", 0},
    //    {"mouthDimple_L", 0},
    //    {"mouthDimple_R", 0},
    //    {"mouthStretch_L", 0},
    //    {"mouthStretch_R", 0},
    //    {"mouthRollLower", 0},
    //    {"mouthRollUpper", 0},
    //    {"mouthShrugLower", 0},
    //    {"mouthShrugUpper", 0},
    //    {"mouthPress_L", 0},
    //    {"mouthPress_R", 0},
    //    {"mouthLowerDown_L", 0},
    //    {"mouthLowerDown_R", 0},
    //    {"mouthUpperUp_L", 0},
    //    {"mouthUpperUp_R", 0},
    //    {"browDown_L", 0},
    //    {"browDown_R", 0},
    //    {"browInnerUp", 0},
    //    {"browOuterUp_L", 0},
    //    {"browOuterUp_R", 0},
    //    {"cheekPuff", 0},
    //    {"cheekSquint_L", 0},
    //    {"cheekSquint_R", 0},
    //    {"tongueOut", 0}
    //};





}
public class SaveFaceShapeVisemes : MonoBehaviour
{
    [SerializeField] List<PhonemePreset> phonemePresets;
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

    
    /*public Dictionary<string, float> Expressions = new Dictionary<string, float>()
    {
        //count 49
        //blendshape count 50
    {"eyeBlink_L",0},
    {"eyeBlink_R", 0},
    {"eyeLookDown_L", 0},
    {"eyeLookDown_R", 0},
    {"eyeLookIn_L", 0},
    {"eyeLookIn_R", 0},
    {"eyeLookOut_L", 0},
    {"eyeLookOut_R", 0},
    {"eyeLookUp_L", 0},
    {"eyeLookUp_R", 0},
    {"eyeSquint_L", 0},
    {"eyeSquint_R", 0},
    {"eyeWide_L", 0},
    {"eyeWide_R", 0},
    {"jawForward", 0},
    {"jawLeft", 0},
    {"jawRight", 0},
    {"jawOpen", 100},
    {"mouthClose", 0},
    {"mouthFunnel", 0},
    {"mouthPucker", 0},
    {"mouthLeft", 0},
    {"mouthRight", 0},
    {"mouthSmile_L", 0},
    {"mouthSmile_R", 0},
    {"mouthFrown_L", 0},
    {"mouthFrown_R", 0},
    {"mouthDimple_L", 0},
    {"mouthDimple_R", 0},
    {"mouthStretch_L", 0},
    {"mouthStretch_R", 0},
    {"mouthRollLower", 0},
    {"mouthRollUpper", 0},
    {"mouthShrugLower", 0},
    {"mouthShrugUpper", 0},
    {"mouthPress_L", 0},
    {"mouthPress_R", 0},
    {"mouthLowerDown_L", 0},
    {"mouthLowerDown_R", 0},
    {"mouthUpperUp_L", 0},
    {"mouthUpperUp_R", 0},
    {"browDown_L", 0},
    {"browDown_R", 0},
    {"browInnerUp", 0},
    {"browOuterUp_L", 0},
    {"browOuterUp_R", 0},
    {"cheekPuff", 0},
    {"cheekSquint_L", 0},
    {"cheekSquint_R", 0},
    {"tongueOut", 0}
        };*/
   

    public Dictionary<string,float> GetCurrentBlendShapes()
    {
        Mesh blendShapeNameMesh = skinnedMeshRenderer.sharedMesh;
        //string[] arr;
        int lengthOfBlendShapes = blendShapeNameMesh.blendShapeCount;
        Dictionary<string, float> blendShapeToReturn=new Dictionary<string, float>(); 
        //arr = new string[lengthOfBlendShapes];
        //string[] keys = new string[Expressions.Keys.Count];
        //Expressions.Keys.CopyTo(keys, 0);

        for (int i= 0; i < lengthOfBlendShapes;i++ )
        {
            string s = blendShapeNameMesh.GetBlendShapeName(i);
            blendShapeToReturn.Add(s.Substring(12), skinnedMeshRenderer.GetBlendShapeWeight(i));
            //arr[i] = s.Substring(12);

            //Debug.Log(s.Substring(12));
            //Debug.Log(keys[i]);
            //Debug.Log(s);
            //if(s.Contains(keys[i]))
            //{
            //    Expressions[s.Substring(12)] = skinnedMeshRenderer.GetBlendShapeWeight(i);
            //}
            //else
            //{

            //    Debug.Log("could not find the key"+ s.Substring(12));
            //}
            
        }



        return blendShapeToReturn;

    }


}


