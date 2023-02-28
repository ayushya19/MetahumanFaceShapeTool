

using System.Collections.Generic;
using UnityEngine;

public class MovingTempScript : MonoBehaviour
{
    public SkinnedMeshRenderer sm;
    // Start is called before the first frame update
    float current = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current=Mathf.MoveTowards(current, 100f, 100f * Time.deltaTime);
        sm.SetBlendShapeWeight(0,current);
    }
}
