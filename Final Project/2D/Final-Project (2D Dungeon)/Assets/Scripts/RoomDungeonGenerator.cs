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

        if(randomWalkRooms)
        {
            floor = CreateRandomRooms(listOfRooms);
        }

        else
        {
            floor = CreateRooms(listOfRooms);
        }

        

        List<Vector2Int> centerOfRooms = new List<Vector2Int>();

        foreach(var rooms in listOfRooms)
        {
            centerOfRooms.Add((Vector2Int)Vector3Int.RoundToInt(rooms.center));
        }

        HashSet<Vector2Int> corridors = Connect(centerOfRooms);

        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTile(floor);
        DungeonWallGenerator.Create(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRandomRooms(List<BoundsInt> listOfRooms)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        for(int i = 0; i < listOfRooms.Count; i++)
        {
            var roomBounds = listOfRooms[i];
            var centerOfRoom = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRadomWalk(walkScriptableObject, centerOfRoom);

            foreach(var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + offset) && 
                   position.x <= (roomBounds.xMax - offset) && 
                   position.y >= (roomBounds.yMin - offset) &&
                   position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }

        return floor;
    }

    private HashSet<Vector2Int> Connect(List<Vector2Int> centerOfRooms)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();

        var centerOfCurrentRoom = centerOfRooms[Random.Range(0, centerOfRooms.Count)];

        centerOfRooms.Remove(centerOfCurrentRoom);

        while (centerOfRooms.Count > 0)
        {
            Vector2Int closest = FindClosestPoint(centerOfCurrentRoom, centerOfRooms);

            centerOfRooms.Remove(closest);

            HashSet<Vector2Int> newCorridor = CreateCorridor(centerOfCurrentRoom, closest);

            centerOfCurrentRoom = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int centerOfCurrentRoom, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();

        var position = centerOfCurrentRoom;

        corridor.Add(position);

        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }

            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
        }

        while(position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }

            else if(destination.x < position.x)
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPoint(Vector2Int centerOfCurrentRoom, List<Vector2Int> centerOfRooms)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach (var position in centerOfRooms)
        {
            float currentDistance = Vector2.Distance(position, centerOfCurrentRoom);

            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }

        return closest;
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
