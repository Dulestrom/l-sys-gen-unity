using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlacementHelper
{
    public static List<Direction> FindNext(Vector3Int position, ICollection<Vector3Int> collecton)
    {
        List<Direction> NextDirections = new List<Direction>();
        
        if (collecton.Contains(position + Vector3Int.right))
        {
            NextDirections.Add(Direction.Right);
        }

        if (collecton.Contains(position - Vector3Int.right))
        {
            NextDirections.Add(Direction.Left);
        }

        if (collecton.Contains(position + new Vector3Int(0, 0, 1)))
        {
            NextDirections.Add(Direction.Up);
        }

        if (collecton.Contains(position - new Vector3Int(0, 0, 1)))
        {
            NextDirections.Add(Direction.Down);
        }

        return NextDirections;
    }
}
