using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ProceduralCity/Rule")]
public class Rule : ScriptableObject
{
    // Main Class that will allow us to create the Rules

    public string letter;
    
    [SerializeField]
    private string[] _results = null; // Allows us to create a list of Rules

    [SerializeField]
    private bool _randomResult = false;

    public string GetResults()
    {
        if (_randomResult)
        {
            int randomIndex = UnityEngine.Random.Range(0, _results.Length);
            return _results[randomIndex];
        }
        return _results[0];
    }
}
