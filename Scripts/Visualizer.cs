using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SimpleVisualizer;

public class Visualizer : MonoBehaviour
{
    // Class that will draw the visuals

    public LSysGen lsystem;
    List<Vector3> positions = new List<Vector3>();
    public RoadHelp roadHelp;

    // Parameters can be changed in order to create something new

    [SerializeField]
    private int _length = 8;

    [SerializeField]
    private float _angle = 90;

    public int Length
    {
        get
        {
            if(_length > 0)
            {
                return _length;
            }
            else
            {
                return 1;
            }
        }
        set => _length = value;
    }

    private void Start()
    {
        var sequence = lsystem.GenerateSentence();
        VisualizeSeq(sequence);
    }

    private void VisualizeSeq(string sequence)
    {
        Stack<AgentParameters> savepoints = new Stack<AgentParameters>();
        var currentPos = Vector3.zero; // Can also use GameObject parameter

        Vector3 direction = Vector3.forward; // Z-axis
        Vector3 tempPosition = Vector3.zero;

        positions.Add(currentPos);

        foreach (var letter in sequence)
        {
            EncLetters encoding = (EncLetters)letter;
            switch (encoding)
            {
                case EncLetters.save:
                    savepoints.Push(new AgentParameters
                    {
                        position = currentPos,
                        direction = direction,
                        length = Length
                    });
                    break;

                case EncLetters.load:
                    if (savepoints.Count > 0)
                    {
                        var agentParameter = savepoints.Pop();
                        currentPos = agentParameter.position;
                        direction = agentParameter.direction;
                        Length = agentParameter.length;
                    }
                    else
                    {
                        throw new System.Exception("Save point not in stack!");
                    }
                    break;

                case EncLetters.draw:
                    tempPosition = currentPos;
                    currentPos += direction * _length;
                    roadHelp.PlaceStreetPos(tempPosition, Vector3Int.RoundToInt(direction), _length);
                    Length -= 2; // This can be upgraded later :)
                    positions.Add(currentPos);
                    break;

                case EncLetters.turnRight:
                    direction = Quaternion.AngleAxis(_angle, Vector3.up) * direction;
                    break;

                case EncLetters.turnLeft:
                    direction = Quaternion.AngleAxis(-_angle, Vector3.up) * direction;
                    break;

                default:
                    break;
            }
        }
        roadHelp.FixRoad();
    }
}
