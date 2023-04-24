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

        List<Vector2Int> DeadEnd = FindAllDeadEnds(floorPositions);

        CreateRoomAtDeadEnd(DeadEnd, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTile(floorPositions);

        DungeonWallGenerator.Create(floorPositions, tilemapVisualizer);
    }

    private void CreateRoomAtDeadEnd(List<Vector2Int> DeadEnd, HashSet<Vector2Int> roomFloors)
    {
        foreach(var position in DeadEnd)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRadomWalk(walkScriptableObject, position);

                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> DeadEnds = new List<Vector2Int>();

        foreach(var position in floorPositions)
        {
            int neighborCount = 0;

            foreach(var direction in Direction2D.cardinalDirectionList)
            {
                if(floorPositions.Contains(position + direction))
                {
                    neighborCount++;
                }
            }

            if (neighborCount == 1)
            {
                DeadEnds.Add(position);
            }
        }

        return DeadEnds;
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
