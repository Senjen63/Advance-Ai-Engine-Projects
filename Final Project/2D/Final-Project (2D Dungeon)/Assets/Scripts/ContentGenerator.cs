using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

//Looked at how to video

public static class ContentGenerator
{
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);

        var previousPosition = startPosition;

        for(int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }

    public static List<Vector2Int> RandomCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();

        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;

        corridor.Add(currentPosition);

        for(int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;

            corridor.Add(currentPosition);
        }

        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int miniumWidth, int miniumHeight)
    {
        Queue<BoundsInt> roomQueued = new Queue<BoundsInt>();
        List<BoundsInt> listOfRooms = new List<BoundsInt>();

        roomQueued.Enqueue(spaceToSplit);

        while(roomQueued.Count > 0)
        {
            var room = roomQueued.Dequeue();

            if(room.size.x >= miniumWidth && room.size.y >= miniumHeight)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.x >= miniumWidth * 2)
                    {
                        SplitRoomVertically(miniumWidth, roomQueued, room);
                    }

                    else if(room.size.y >= miniumHeight * 2)
                    {
                        SplitRoomHorizontally(miniumHeight, roomQueued, room);
                    }

                    else if(room.size.x >= miniumWidth * 2 && room.size.y >= miniumHeight * 2)
                    {
                        listOfRooms.Add(room);
                    }
                }
            }

            else
            {
                if (room.size.y >= miniumHeight * 2)
                {
                    SplitRoomHorizontally(miniumHeight, roomQueued, room);
                }

                else if (room.size.x >= miniumWidth * 2)
                {
                    SplitRoomVertically(miniumWidth, roomQueued, room);
                }
                             

                else if (room.size.x >= miniumWidth * 2 && room.size.y >= miniumHeight * 2)
                {
                    listOfRooms.Add(room);
                }
            }
        }

        return listOfRooms;
    }

    public static void SplitRoomHorizontally(int miniumHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var splitY = Random.Range(1, room.size.y);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, splitY, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + splitY, room.size.z), new Vector3Int(room.size.x, room.size.y - splitY, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    public static void SplitRoomVertically(int miniumWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var splitX = Random.Range(1, room.size.x);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(splitX, room.min.y, room.min.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + splitX, room.min.y, room.min.z), new Vector3Int(room.size.x - splitX, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //Right
        new Vector2Int(0, -1), //Down
        new Vector2Int(-1, 0), //Left
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
