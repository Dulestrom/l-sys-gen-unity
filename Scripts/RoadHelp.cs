using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadHelp : MonoBehaviour
{
    public GameObject roadStraight, roadCorner, roadThreeWay, roadFourWay, roadEnd;
    Dictionary<Vector3Int, GameObject> roadDict = new Dictionary<Vector3Int, GameObject>();
    HashSet<Vector3Int> fixRoadCandidates = new HashSet<Vector3Int>();

    public void PlaceStreetPos(Vector3 startPos, Vector3Int direction, int length)
    {
        var rotation = Quaternion.identity; // Will change later for now just identity
        
        if (direction.x == 0)
        {
            rotation = Quaternion.Euler(0, 90, 0);
        }

        for (int i = 0; i < length; i++)
        {
            var position = Vector3Int.RoundToInt(startPos + direction * i); // next position for road
            
            if (roadDict.ContainsKey(position))
            {
                continue;
            }

            var road = Instantiate(roadStraight, position, rotation, transform);
            roadDict.Add(position, road);

            if (i == 0 || i == length - 1)
            {
                fixRoadCandidates.Add(position);
            }
        }
    }

    public void FixRoad()
    {
        foreach (var position in fixRoadCandidates)
        {
            List<Direction> NextDirections = PlacementHelper.FindNext(position, roadDict.Keys);

            Quaternion rotation = Quaternion.identity;

            if (NextDirections.Count == 1)
            {
                Destroy(roadDict[position]);
                if (NextDirections.Contains(Direction.Down))
                {
                    rotation = Quaternion.Euler(0, 90, 0);
                }

                else if (NextDirections.Contains(Direction.Left))
                {
                    rotation = Quaternion.Euler(0, 180, 0);
                }

                else if (NextDirections.Contains(Direction.Up))
                {
                    rotation = Quaternion.Euler(0, -90, 0);
                }

                roadDict[position] = Instantiate(roadEnd, position, rotation, transform);
            }

            else if (NextDirections.Count == 2)
            {
                if(NextDirections.Contains(Direction.Up) && NextDirections.Contains(Direction.Down) || NextDirections.Contains(Direction.Right) && NextDirections.Contains(Direction.Left))
                {
                    continue;
                }

                Destroy(roadDict[position]);

                if (NextDirections.Contains(Direction.Up) && NextDirections.Contains(Direction.Right))
                {
                    rotation = Quaternion.Euler(0, 90, 0);
                }

                else if (NextDirections.Contains(Direction.Right) && NextDirections.Contains(Direction.Down))
                {
                    rotation = Quaternion.Euler(0, 180, 0);
                }

                else if (NextDirections.Contains(Direction.Down) && NextDirections.Contains(Direction.Left))
                {
                    rotation = Quaternion.Euler(0, -90, 0);
                }

                roadDict[position] = Instantiate(roadCorner, position, rotation, transform);

            }

            else if (NextDirections.Count == 3)
            {
                Destroy(roadDict[position]);

                if (NextDirections.Contains(Direction.Right) && 
                    NextDirections.Contains(Direction.Down) && 
                    NextDirections.Contains(Direction.Left))
                {
                    rotation = Quaternion.Euler(0, 90, 0);
                }

                else if (NextDirections.Contains(Direction.Down) && 
                         NextDirections.Contains(Direction.Left) && 
                         NextDirections.Contains(Direction.Up))
                {
                    rotation = Quaternion.Euler(0, 180, 0);
                }

                else if (NextDirections.Contains(Direction.Left) && 
                         NextDirections.Contains(Direction.Up) && 
                         NextDirections.Contains(Direction.Right))
                {
                    rotation = Quaternion.Euler(0, -90, 0);
                }

                roadDict[position] = Instantiate(roadThreeWay, position, rotation, transform);
            }

            else
            {
                Destroy(roadDict[position]);
                roadDict[position] = Instantiate(roadFourWay, position, rotation, transform);
            }
        }
    }
}
