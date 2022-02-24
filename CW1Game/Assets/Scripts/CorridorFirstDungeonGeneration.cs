using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGeneration : SimpleRandomWalkMapGen
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roompercent;
    protected override List<Vector2Int> RunProceduralGeneration()
    {
        List<Vector2Int> floorspace = CorridorFirstGeneration();
        return floorspace;
    }

    private List<Vector2Int> CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> possibleRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, possibleRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(possibleRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        List<Vector2Int> floorspace = floorPositions.ToList();
        return floorspace;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var room in deadEnds)
        {
            if (!roomPositions.Contains(room))
            {
                var roomFloor = RunRandomWalk(randomWalkParameters, room);
                roomPositions.UnionWith(roomFloor);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(pos + direction))
                {
                    neighboursCount++;
                }
            }
            if(neighboursCount == 1)
            {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> possibleRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(possibleRoomPositions.Count * roompercent);

        List<Vector2Int> roomToCreate = possibleRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        foreach (var room in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, room);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> possibleRoomPositions)
    {
        var currentPosition = startPosition;
        possibleRoomPositions.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGeneration.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            possibleRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);

        }
    }
}
