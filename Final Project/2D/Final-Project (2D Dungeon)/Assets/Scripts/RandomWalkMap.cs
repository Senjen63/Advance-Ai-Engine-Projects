using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

//referenced tutorial
public class RandomWalkMap : AbstractDungeon
{
    [SerializeField]
    private RandomWalkScriptableObject walkScriptableObject;

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

        for(int i = 0; i < walkScriptableObject.iterations; i++)
        {
            var path = ContentGenerator.RandomWalk(currentPosition, walkScriptableObject.walkLength);
            floorPositions.UnionWith(path);

            if(walkScriptableObject.StartRandomlyIterating)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }
}
