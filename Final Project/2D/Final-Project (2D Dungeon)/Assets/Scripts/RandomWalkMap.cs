using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkMap : AbstractDungeon
{
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLenght = 10;
    [SerializeField]
    public bool isStartrandomlyIterating = true;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRadomWalk();

        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTile(floorPositions);
    }

    protected HashSet<Vector2Int> RunRadomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for(int i = 0; i < iterations; i++)
        {
            var path = ContentGenerator.RandomWalk(currentPosition, walkLenght);
            floorPositions.UnionWith(path);

            if(isStartrandomlyIterating)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }
}
