using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
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
                    Debug.Log(bsw.Key + "  is changing");
                    currentBlendShapeWeight=skinnedMeshRenderer.GetBlendShapeWeight(i);
                    valueToReturn=Mathf.Lerp(currentBlendShapeWeight, bsw.Value, Time.deltaTime*0.5f);
                    StartCoroutine(ExampleCoroutine());
                    Debug.Log("From "+currentBlendShapeWeight+"To "+bsw.Value+" current Value  " + valueToReturn);
                    skinnedMeshRenderer.SetBlendShapeWeight(i,valueToReturn*100);
                    i++;
                }
                
            }

        }
        
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);



        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
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
