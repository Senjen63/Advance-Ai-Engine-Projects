using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDungeonGenerator : RandomWalkMap
{
    [SerializeField]
    private int minRoomWidth = 4;

    [SerializeField]
    private int minRoomHeight = 4;

    [SerializeField]
    private int dungeonWidth = 20;

    [SerializeField]
    private int dungeonHeight = 20;

    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;

    [SerializeField]
    private bool randomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var listOfRooms = ContentGenerator.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, 
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        floor = CreateRooms(listOfRooms);

        tilemapVisualizer.PaintFloorTile(floor);
        DungeonWallGenerator.Create(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRooms(List<BoundsInt> listOfRooms)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in listOfRooms)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);

                    floor.Add(position);
                }
            }
        }
        return floor;
    }


}
