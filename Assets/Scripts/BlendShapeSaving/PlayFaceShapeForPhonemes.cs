using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;


public class PlayFaceShapeForPhonemes : MonoBehaviour
{
    // Start is called before the first frame update
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public char phonemeSeperator;
    public char wordSeperator;
    public string sentenceToSpeak;


    private List<PhonemePreset> phonemePresets=new List<PhonemePreset>();
    private List<PhonemePreset> phonemeToSpeak=new List<PhonemePreset>();


    private List<string> phonemeList=new List<string>();
    private List<string> currentPhoneme=new List<string>();
    private List<List<string>> wordList=new List<List<string>>();

    int i = 0;
    private float fromLerp, toLerp;
    public float speedOFChange;
    private bool startLerping = false;
    private int frame;

    public MovingTempScript mvtScript;

    private void Start()
    {
        //foreach (string filename in files)
        
        GetTheJsonFileAsDictionary(); 

    }

    private void Update()
    {
        //Debug.Log("frame " + frame);

        //foreach (PhonemePreset pp in phonemeToSpeak)
        //{
        //    Debug.Log("Speaking " + pp.Visemes);
        //    SpeakOutThePhoneme(pp);

        //}
        //frame++;
    }


   
    void SpeakOutThePhoneme(PhonemePreset pp)
    {
        i = 0;
        foreach (KeyValuePair<string, float> kvp in pp.Expressions)
        {

            fromLerp = skinnedMeshRenderer.GetBlendShapeWeight(i);
            toLerp = kvp.Value;
            Debug.Log("for "+kvp.Key+"from " + fromLerp + "  to  " + toLerp);
           
             StartCoroutine(MoveTheBlendShapes()); 
            i++;
        }
        if(fromLerp==toLerp)
        { return;  }
    }

   





    public IEnumerator MoveTheBlendShapes()
    {
        
          //yield return(new WaitForEndOfFrame());
            Debug.Log("lerping");
            fromLerp = Mathf.MoveTowards(fromLerp, toLerp, 1);
            Debug.Log(fromLerp);
            skinnedMeshRenderer.SetBlendShapeWeight(i, fromLerp);
        
        yield return null;

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


                FindMatchingPhonemes(phoneme);
            }

        }
        //SpeakOutThosePhonemesIndividually();
        mvtScript.GetPlayerReady(phonemeToSpeak);
    }

    void FindMatchingPhonemes(string phoneme)
    {
        
        foreach(PhonemePreset pp in phonemePresets)
        {
            if(pp.Visemes.Equals(phoneme))
            {
                Debug.Log(phoneme+"is added");
                PhonemePreset temp = new PhonemePreset();
                temp.Visemes = pp.Visemes;
                temp.Expressions = pp.Expressions;
                
                phonemeToSpeak.Add(temp);
            }

        }

        




    }





   



    public static class WaitFor
    {
        public static IEnumerator Frames(int frameCount)
        {
            if (frameCount <= 0)
            {
                throw new ArgumentOutOfRangeException("frameCount", "Cannot wait for less that 1 frame");
            }

            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }
        }
    }

    void GetTheJsonFileAsDictionary()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json", SearchOption.AllDirectories);
        foreach (string fileN in files)
        { 
            if (File.Exists(fileN))
            {
                string fileContents = File.ReadAllText(fileN);
                phonemePresets.Add(JsonConvert.DeserializeObject<PhonemePreset>(fileContents));
            } 
        }

        BreakPhonemeDownToExpressions(sentenceToSpeak);
    }

    


}
