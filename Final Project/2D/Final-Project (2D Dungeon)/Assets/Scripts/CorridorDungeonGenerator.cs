using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public Text length;
    public Text count;
    public Text room;

    protected override void RunProceduralGeneration()
    {
        CorridorDungeonGeneration();
    }

    private void CorridorDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = Create(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> DeadEnd = FindAllDeadEnds(floorPositions);

        CreateRoomAtDeadEnd(DeadEnd, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for(int i = 0; i < corridors.Count; i++)
        {
            //corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            corridors[i] = IncreaseCorridorBrush3By3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

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

    private List<List<Vector2Int>> Create(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;

        potentialRoomPositions.Add(currentPosition);

        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for(int i =0; i < corridorCount; i++)
        {
            var corridor = ContentGenerator.RandomCorridor(currentPosition, corridorLength);

            corridors.Add(corridor);

            currentPosition = corridor[corridor.Count - 1];

            potentialRoomPositions.Add(currentPosition);

            floorPositions.UnionWith(corridor);
        }

        return corridors;
    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;

        for(int i = 1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];

            if(previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                //handle corner
                for(int x = -1; x < 2; x++)
                {
                    for(int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }

                previousDirection = directionFromCell;
            }

            else
            {
                //Add a single cell in the direction + 90 degrees
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);

                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }

        return newCorridor;
    }

    public List<Vector2Int> IncreaseCorridorBrush3By3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();

        for(int i = 1; i < corridor.Count; i++)
        {
            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }

        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
        {
            return Vector2Int.right;
        }

        if(direction == Vector2Int.right)
        {
            return Vector2Int.down;
        }

        if(direction == Vector2Int.down)
        {
            return Vector2Int.left;
        }

        if(direction == Vector2Int.left)
        {
            return Vector2Int.up;
        }

        return Vector2Int.zero;
    }

    public void IncreaseCorridorLength()
    {
        corridorLength++;
    }

    public void DecreaseCorridorLength()
    {
        corridorLength--;

        if (corridorLength <= 0)
        {
            corridorLength = 0;
        }
    }

    public void IncreaseCorridorCount()
    {
        corridorCount++;
    }

    public void DecreaseCorridorCount()
    {
        corridorCount--;

        if (corridorCount <= 0)
        {
            corridorCount = 0;
        }
    }

    public void IncreaseRoomPercent()
    {
        roomPercent += 0.1f;

        if(roomPercent >= 1)
        {
            roomPercent = 1;
        }
    }

    public void DecreaseRoomPercent()
    {
        roomPercent -= 0.1f;

        if (roomPercent <= 0)
        {
            roomPercent = 0;
        }
    }

    public void Update()
    {
        length.text = "Corridor Length: " + corridorLength.ToString();
        count.text = "Corridor Count: " + corridorCount.ToString();
        room.text = "Room Percent: " + roomPercent.ToString();
    }
}
