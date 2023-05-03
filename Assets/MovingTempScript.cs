

using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MovingTempScript : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public List<Dictionary<string, float>> blendShapeValues=new List<Dictionary<string, float>>(); // list of blendshape dictionaries
    public float animationDuration = 1f; // how long the animation should take
    private Dictionary<string, float> currentValues; // current blendshape values
    private Dictionary<string, float> targetValues; // target blendshape values
    public TMP_Text whatIsSpeakin;
    
    private List<Dictionary<string, float[]>> differences = new List<Dictionary<string,float[]>>();
    private float[] currentDifference=new float[2];
    Dictionary<string, float[]> currentDifferenceDictionary = new Dictionary<string, float[]>();
    private bool dictionaryFilled = false;
    float animationStartTime = 0f;

    private int toCount = 0;
    public void GetPlayerReady(List<PhonemePreset> phonemePresetToSpeak)
    {
        for (int j = 0; j < phonemePresetToSpeak.Count; j++)
        {
           
            blendShapeValues.Add(new Dictionary<string, float>());
            foreach (KeyValuePair<string, float> kvp in phonemePresetToSpeak[j].Expressions)
            {
                blendShapeValues[j].Add(kvp.Key, kvp.Value);
               
            }

        }
        currentValues = new Dictionary<string, float>();
        targetValues = new Dictionary<string, float>();
        foreach (var pair in blendShapeValues[0])
        {
            int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(pair.Key);
            currentValues[pair.Key] = skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
            targetValues[pair.Key] = pair.Value;
        }
        PhonemePreset currPhope = new PhonemePreset();
        PhonemePreset nextPhope = new PhonemePreset();
        foreach (PhonemePreset phope in phonemePresetToSpeak)
        {
            

            
            nextPhope.Expressions = phope.Expressions;
            nextPhope.Visemes = phope.Visemes;
            
            if (currPhope.Expressions.Count<=0)
            {
                currPhope.Visemes = "";
                foreach (KeyValuePair<string, float> kvp in nextPhope.Expressions)
                {
                    
                    currPhope.Expressions.Add(kvp.Key, 0);
                    
                }

            }

            FindDifferencebetweenDictionary(currPhope.Expressions, nextPhope.Expressions);
            
            foreach(KeyValuePair<string,float> kvp in nextPhope.Expressions)
            {

            currPhope.Expressions[kvp.Key] = nextPhope.Expressions[kvp.Key];
                
            }
        }
        dictionaryFilled = true;
        
        foreach(Dictionary<string,float[]> toShow in differences)
        { toCount++;
           
            Debug.Log(toCount);
           foreach(KeyValuePair<string,float[]> kvp in toShow)
            {
                Debug.Log(kvp.Key + " is the difference key from" + kvp.Value[0] + "  To" + kvp.Value[1]);
            }
        }
    }

    void FindDifferencebetweenDictionary(Dictionary<string,float> dictionary1,Dictionary<string,float> dictionary2)
    {
            
        
        foreach (KeyValuePair<string, float> kvp in dictionary1)
        {
            if (dictionary1[kvp.Key] == dictionary2[kvp.Key]) 
            {
               
            }
            else
            {
                //if (dictionary1[kvp.Key] < dictionary2[kvp.Key])
                //{
                //    currentDifference[0] = dictionary1[kvp.Key];
                //    currentDifference[1] = dictionary2[kvp.Key];
                //}
                //else if (dictionary1[kvp.Key] > dictionary2[kvp.Key])
                //{
                //    currentDifference[1] = dictionary1[kvp.Key];
                //    currentDifference[0] = dictionary2[kvp.Key];

                //}
                currentDifference[0] = dictionary1[kvp.Key];
                   currentDifference[1] = dictionary2[kvp.Key];

                float[] tempFloatList = new float[2];
                tempFloatList[0] = currentDifference[0];
                tempFloatList[1] = currentDifference[1];
                currentDifferenceDictionary.Add(kvp.Key,tempFloatList);
                

               

            }

            
                //Debug.Log(currentDifferenceDictionary.Count);
            
        }
               
                
                
                differences.Add(new Dictionary<string, float[]>(currentDifferenceDictionary));
                currentDifferenceDictionary.Clear();
       
    }



    private void Update()
    {
        if (dictionaryFilled)
        {
            float animationProgress = Mathf.Clamp01((Time.time - animationStartTime) / animationDuration);
        
            int justCounting = 0;
            foreach (Dictionary<string, float[]> difference in differences)
            { justCounting++;
                //Debug.Log(difference.Count);
                foreach (KeyValuePair<string, float[]> singleDifference in difference)
                {
                    int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("blendShape2." + singleDifference.Key);
                    
                    float valueToPut = Mathf.Lerp(singleDifference.Value[0], singleDifference.Value[1], animationProgress);
                    skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, valueToPut);
                    Debug.Log(justCounting + "  Iteration from " + singleDifference.Value[0] + " to " + singleDifference.Value[1] + "Currently it is " + valueToPut+ " at "+animationProgress);
                }


            }
            if (animationProgress == 1f)
            {
                animationStartTime = Time.time;
            }

        }
    }























    //ON next iteration the current values are not gettin loaded
    //void Update()
    //{
    //    Debug.Log(frame++);

    //    if (blendShapeValues != null && blendShapeValues.Count > 0)
    //    {
    //        // calculate the progress of the animation
    //        float animationProgress = Mathf.Clamp01((Time.time - animationStartTime) / animationDuration);

    //        // update blendshape values
    //        foreach (var pair in currentValues)
    //        {
    //            string blendShapeName = pair.Key;
    //            float current = pair.Value;
    //            float target = targetValues[blendShapeName];
    //            if (current < target)
    //            {
    //                int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("blendShape2." + blendShapeName);
    //                Debug.Log("For "+ blendShapeIndex + "  blend shape index  current value is "+ current  + " and the target value is "+ target);
    //                float newValue = Mathf.Lerp(current, target, animationProgress);
    //                Debug.Log(newValue + " is the new value");

    //                skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, newValue);

    //                currentValues[blendShapeName] = newValue;
    //            }
    //        }

    //        // check if animation is finished
    //        if (animationProgress == 1f)
    //        {
    //            // set new target values for the next animation

    //            whatIsSpeakin.text="   "+ index++ + " is spoken";
    //            targetValues.Clear();
    //            currentValues.Clear();
    //            blendShapeValues.RemoveAt(0);
    //            if (blendShapeValues.Count > 0)
    //            {
    //                var nextDictionary = blendShapeValues[0];
    //                foreach (var pair in nextDictionary)
    //                {
    //                    targetValues[pair.Key] = pair.Value;
    //                    int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("blendShape2."+pair.Key);
    //                    currentValues[pair.Key] = skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
    //                }
    //                animationStartTime = Time.time;
    //            }
    //        }
    //    }



    //}



}
