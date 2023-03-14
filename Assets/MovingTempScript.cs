

using System.Collections.Generic;
using UnityEngine;

public class MovingTempScript : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
  
    public float transitionTime; // The amount of time it takes to transition from dictionary1 to dictionary2

    // A temporary dictionary to store the intermediate values
    private float transitionTimer; // A timer to track the progress of the transition
    int i = 0;
    private float temp;
    private float currentMaxVal = 100f;
    int j = 0;
    private bool startLerping = false;


    public List<Dictionary<string, float>> blendShapeValues; // list of blendshape dictionaries
    public float animationDuration = 1f; // how long the animation should take
    //public SkinnedMeshRenderer skinnedMeshRenderer; // reference to the SkinnedMeshRenderer

    private Dictionary<string, float> currentValues; // current blendshape values
    private Dictionary<string, float> targetValues; // target blendshape values
    private float animationStartTime; // time the animation started

    

    public void GetPlayerReady(List<PhonemePreset> phonemePresetToSpeak)
    {
        for (int j = 0; j < phonemePresetToSpeak.Count; j++)
        {
            Debug.Log(phonemePresetToSpeak[j].Visemes);
            blendShapeValues.Add(new Dictionary<string, float>());
            foreach (KeyValuePair<string, float> kvp in phonemePresetToSpeak[j].Expressions)
            {
                blendShapeValues[j].Add(kvp.Key, kvp.Value);
                Debug.Log(kvp.Key + "   is key" + kvp.Value + "  is value");
            }

        }
        transitionTimer = 0.0f;
        startLerping = true;

        currentValues = new Dictionary<string, float>();
        targetValues = new Dictionary<string, float>();
        foreach (var pair in blendShapeValues[0])
        {
            int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(pair.Key);
            currentValues[pair.Key] = skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
            targetValues[pair.Key] = pair.Value;
        }
    }





    void Update()
    {

        if (startLerping)
        {
            // calculate the progress of the animation
            float animationProgress = Mathf.Clamp01((Time.time - animationStartTime) / animationDuration);

            // update blendshape values
            foreach (var pair in currentValues)
            {
                string blendShapeName = pair.Key;
                float current = pair.Value;
                float target = targetValues[blendShapeName];
                int newValue = Mathf.RoundToInt(Mathf.Lerp(current, target, animationProgress));
                int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
                skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, newValue);
                currentValues[blendShapeName] = newValue;
            }

            // check if animation is finished
            if (animationProgress == 1f)
            {
                // set new target values for the next animation
                targetValues.Clear();
                currentValues.Clear();
                foreach (var pair in blendShapeValues[0])
                {
                    int blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(pair.Key);
                    currentValues[pair.Key] = skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
                }
                blendShapeValues.RemoveAt(0);
                if (blendShapeValues.Count > 0)
                {
                    foreach (var pair in blendShapeValues[0])
                    {
                        targetValues[pair.Key] = pair.Value;
                    }
                    animationStartTime = Time.time;
                }
            }
        }
    }

}
