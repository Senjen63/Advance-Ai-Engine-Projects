using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//referenced tutorial
public static class DungeonWallGenerator
{
    public static void Create(HashSet<Vector2Int> positions, TilemapVisualizer tilemapVisualizer)
    {
        var positionsOfBasicWalls = FindWalls(positions, Direction2D.cardinalDirectionList);

        foreach(var position in positionsOfBasicWalls)
        {

        }
    }

    private static HashSet<Vector2Int> FindWalls(HashSet<Vector2Int> positions, List<Vector2Int> ListOfDirections)
    {
        HashSet<Vector2Int> walls = new HashSet<Vector2Int>();

        foreach (Vector2Int position in positions)
        {
            foreach(Vector2Int direction in ListOfDirections)
            {
                var positionOfNeighbors = position + direction;

                if(!positions.Contains(positionOfNeighbors))
                {
                    walls.Add(positionOfNeighbors);
                }
            }
        }

        return walls;
    }
}
