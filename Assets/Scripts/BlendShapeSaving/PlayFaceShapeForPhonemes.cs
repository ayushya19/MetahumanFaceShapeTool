using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
public class PlayFaceShapeForPhonemes : MonoBehaviour
{
    // Start is called before the first frame update
    public char phonemeSeperator;
    public char wordSeperator;
    public string sentenceToSpeak;

    private List<string> phonemeList=new List<string>();

    private List<string> currentPhoneme=new List<string>();
    private List<List<string>> wordList=new List<List<string>>();

    

    //public List<string> files;

    public SkinnedMeshRenderer skinnedMeshRenderer;
    private List<PhonemePreset> phonemePresets=new List<PhonemePreset>();
    int i = 0;

    private float fromLerp, toLerp;
        public float speedOFChange;
    private float lerpValueReturned;

    private bool startLerping = false;

    
    private void Start()
    {
        //foreach (string filename in files)
        
        GetTheJsonFileAsDictionary(); 

    }
    void BreakPhonemeDownToExpressions(string phonemes)
    {


        foreach(char c in phonemes)
        {
            
            if(c.Equals(wordSeperator))
            {
                wordList.Add(new List<string>(phonemeList));
                
                phonemeList.Clear();
                currentPhoneme.Clear();
                continue;
            }
            if(c.Equals(phonemeSeperator))
            {

                
                phonemeList.Add(string.Join("",currentPhoneme)) ;
                
                currentPhoneme.Clear();
                continue;
            }
            


            currentPhoneme.Add(c.ToString());
            

        }
            SpeakOutTheExpressions();

    }




    void SpeakOutTheExpressions()
    {
                
        foreach(List<string> word in wordList)
        {
            foreach(string phoneme in word)
            {
              
                
                speakPhoneme(phoneme);
            }

        }
    }

    void speakPhoneme(string phoneme)
    {
        float currentBlendShapeWeight = 0f;
        float valueToReturn = 0f;
        foreach(PhonemePreset pp in phonemePresets)
        {
            if(pp.Visemes.Equals(phoneme))
            {
                Debug.Log(phoneme+"is spoken");

                i = 0;
                foreach(KeyValuePair<string, float> bsw in pp.Expressions)
                { 
                        currentBlendShapeWeight = skinnedMeshRenderer.GetBlendShapeWeight(i);
                        startLerping = true;
                        fromLerp = currentBlendShapeWeight;
                        toLerp = bsw.Value;
                        Debug.Log(bsw.Key + "  is changing from " + currentBlendShapeWeight + " to " + toLerp);
                        StartCoroutine(MoveTheBlendShapes(1f));

                        i++;
                    
                }
                
            }

        }
        


    }

    private void Update()
    {
        
            
            
    }

    public IEnumerator MoveTheBlendShapes(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            
            skinnedMeshRenderer.SetBlendShapeWeight(i, Time.deltaTime * speedOFChange);

            
            yield return null;

        }

    }


   

    
    void GetTheJsonFileAsDictionary()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json", SearchOption.AllDirectories);
        
        int i = 0;
        float randomValue;
        foreach (string fileN in files)
        { 
            if (File.Exists(fileN))
            {
                // Read the entire file and save its contents.
                string fileContents = File.ReadAllText(fileN);
                //Debug.Log(fileContents);
                phonemePresets.Add(JsonConvert.DeserializeObject<PhonemePreset>(fileContents));


                //phonemePresets[i].Expressions.TryGetValue("eyeBlink_L", out randomValue);
                //Debug.Log(randomValue);
                //int j = 0;
                
                //foreach(KeyValuePair<string,float> bS in phonemePresets[i].Expressions)
                //{
                //    Debug.Log("going here");
                //    Debug.Log(bS.Value);
                //    skinnedMeshRenderer.SetBlendShapeWeight(j, bS.Value);
                //    j++;
                //}
                //i++;
                
                //// Work with JSON
            } 
        }

        BreakPhonemeDownToExpressions(sentenceToSpeak);
    }

    


}
