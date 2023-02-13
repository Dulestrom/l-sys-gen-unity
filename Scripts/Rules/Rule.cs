using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ProceduralCity/Rule")]
public class Rule : ScriptableObject
{
    public string letter;
    
    [SerializeField]
    private string[] _results = null;

    public string GetResults()
    {
        return _results[0];
    }
}
