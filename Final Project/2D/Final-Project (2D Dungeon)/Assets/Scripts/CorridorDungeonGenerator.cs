using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Looked at how to video

public class CorridorDungeonGenerator : RandomWalkMap
{
    [SerializeField]
    private int corridorLength = 14;

    [SerializeField]
    private int corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorDungeonGeneration();
    }

    private void CorridorDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        Create(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTile(floorPositions);

        DungeonWallGenerator.Create(floorPositions, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach(var roomPosition in roomToCreate)
        {
            var roomFloor = RunRadomWalk(walkScriptableObject, roomPosition);

            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void Create(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;

        potentialRoomPositions.Add(currentPosition);

        for(int i =0; i < corridorCount; i++)
        {
            var corridor = ContentGenerator.RandomCorridor(currentPosition, corridorLength);

            currentPosition = corridor[corridor.Count - 1];

            potentialRoomPositions.Add(currentPosition);

            floorPositions.UnionWith(corridor);
        }
    }
}
