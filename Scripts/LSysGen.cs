using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LSysGen : MonoBehaviour
{
    public Rule[] rules;
    public string axiom;
    [Range(0, 10)]
    public int iterLim = 1;

    private void Start()
    {
        Debug.Log(GenerateSentence());
    }

    public string GenerateSentence(string word = null)
    { 

        if (word == null)
        {
            word = axiom;
        }

        return GrowRecursive(word);
    }

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

    private void ProcessRulesRecursively(StringBuilder newWord, char c, int iterIndex)
    {
        foreach (var rule in rules)
        {
            if (rule.letter == c.ToString())
            {
                newWord.Append(GrowRecursive(rule.GetResults(), iterIndex + 1));
            }
        }
    }
}
