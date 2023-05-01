using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Looked at how to video
public static class DungeonWallGenerator
{
    public static void Create(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var positionsOfBasicWalls = FindWalls(floorPositions, Direction2D.cardinalDirectionList);

        var cornersOfWalls = FindWalls(floorPositions, Direction2D.diagonalDirectionList);

        CreateBasic(tilemapVisualizer, positionsOfBasicWalls, floorPositions);

        CreateCorner(tilemapVisualizer, positionsOfBasicWalls, floorPositions);
    }

    private static void CreateBasic(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> positionsOfBasicWalls, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in positionsOfBasicWalls)
        {
            string neighborsBinary = "";

            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                var neighborPosition = position + direction;

                if(floorPositions.Contains(neighborPosition))
                {
                    neighborsBinary += "1";
                }

                else
                {
                    neighborsBinary += "0";
                }
            }

            tilemapVisualizer.PaintWall(position, neighborsBinary);
        }
    }

    private static void CreateCorner(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornersOfWalls, HashSet<Vector2Int> floorPositions)
    {
        foreach(var position in cornersOfWalls)
        {
            string neighborsBinary = "";

            foreach(var direction in Direction2D.eightDirectionsList)
            {
                var neighborPosition = position + direction;

                if(floorPositions.Contains(neighborPosition))
                {
                    neighborsBinary += "1";
                }

                else
                {
                    neighborsBinary += "0";
                }
            }

            tilemapVisualizer.PaintCornerWall(position, neighborsBinary);
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
