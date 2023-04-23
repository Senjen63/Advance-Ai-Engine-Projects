using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Looked at how to video

public class CorridorDungeonGenerator : RandomWalkMap
{
    [SerializeField]
    private int corridorLength = 14;

    [SerializeField]
    private int corridorCount = 5;

    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    [SerializeField]
    public RandomWalkScriptableObject roomGeneratorParameters;

    protected override void RunProceduralGeneration()
    {
        CorridorDungeonGeneration();
    }

    private void CorridorDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        Create(floorPositions);

        tilemapVisualizer.PaintFloorTile(floorPositions);

        DungeonWallGenerator.Create(floorPositions, tilemapVisualizer);
    }

    private void Create(HashSet<Vector2Int> floorPositions)
    {
        var currentPosition = startPosition;

        for(int i =0; i < corridorCount; i++)
        {
            var corridor = ContentGenerator.RandomCorridor(currentPosition, corridorLength);

            currentPosition = corridor[corridor.Count - 1];
            floorPositions.UnionWith(corridor);
        }
    }
}
