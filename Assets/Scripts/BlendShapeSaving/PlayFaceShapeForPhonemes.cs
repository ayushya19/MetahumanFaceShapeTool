using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFaceShapeForPhonemes : MonoBehaviour
{
    // Start is called before the first frame update
    public string phonemeSeperator;
    public string wordSeperator;
    private List<string> phonemeList;

    private List<char> currentPhoneme;
    private List<List<string>> wordList;
    void Start()
    {
        
    }

    void BreakPhonemeDownToExpressions(string phonemes)
    {
        foreach(char c in phonemes)
        {
            if(c.Equals(wordSeperator))
            {
                wordList.Add(phonemeList);
                phonemeList.Clear();
            }
            if(c.Equals(phonemeSeperator))
            {
                phonemeList.Add(currentPhoneme.ToString());
                currentPhoneme.Clear();
            }
            


            currentPhoneme.Add(c);





        }
    }

    void SpeakOutTheExpressions()
    {
        
    }
}
