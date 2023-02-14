using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LSysGen : MonoBehaviour
{
    // Class that will create the sentence for the L-System
    public Rule[] rules;
    public string axiom;
    [Range(0, 10)]
    public int iterLim = 1;

    public bool randomIgnoreRuleMod = true;

    [Range(0, 1)]
    public float chanceToIgnore = 0.3f;

    private void Start()
    {
        Debug.Log(GenerateSentence()); // Just to see what the output is
    }

    // Generated Sentence
    public string GenerateSentence(string word = null)
    { 

        if (word == null)
        {
            word = axiom;
        }

        return GrowRecursive(word);
    }
    
    // Recursively grows the sentence based on the Rules
    private string GrowRecursive(string word, int iterIndex = 0)
    {

        if (iterIndex >= iterLim)
        {
            return word;
        }

        StringBuilder newWord = new StringBuilder();

        foreach (var c in word)
        {
            newWord.Append(c);
            ProcessRulesRecursively(newWord, c, iterIndex);
        }

        return newWord.ToString();
    }

    // Processes rules and adds the letters to the New Word
    private void ProcessRulesRecursively(StringBuilder newWord, char c, int iterIndex)
    {
        foreach (var rule in rules)
        {
            if (rule.letter == c.ToString())
            {
                if (randomIgnoreRuleMod && iterIndex > 1)
                {
                    if (UnityEngine.Random.value < chanceToIgnore)
                    {
                        return;
                    }
                }
                newWord.Append(GrowRecursive(rule.GetResults(), iterIndex + 1));
            }
        }
    }
}
