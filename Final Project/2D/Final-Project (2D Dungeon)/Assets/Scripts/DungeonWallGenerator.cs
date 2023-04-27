using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Looked at how to video
public static class DungeonWallGenerator
{
    public static void Create(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var positionsOfBasicWalls = FindWalls(floorPositions, Direction2D.cardinalDirectionList);

        foreach(var position in positionsOfBasicWalls)
        {
            tilemapVisualizer.PaintWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWalls(HashSet<Vector2Int> floorPositions, List<Vector2Int> ListOfDirections)
    {
        HashSet<Vector2Int> walls = new HashSet<Vector2Int>();

        foreach (Vector2Int position in floorPositions)
        {
            foreach(Vector2Int direction in ListOfDirections)
            {
                var positionOfNeighbors = position + direction;

                if(floorPositions.Contains(positionOfNeighbors) == false)
                {
                    walls.Add(positionOfNeighbors);
                }
            }
        }

        return walls;
    }
}
