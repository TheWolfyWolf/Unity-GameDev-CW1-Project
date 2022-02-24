using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindwallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var conerWallPosition = FindwallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateCornerWalls(tilemapVisualizer, conerWallPosition, floorPositions);
        CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);

    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> conerWallPosition, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in conerWallPosition)
        {
            string neighboursBinary = "";
            foreach (var direction in Direction2D.DirectionsList)
            {
                var neighbourPos = position + direction;
                if (floorPositions.Contains(neighbourPos))
                {
                    neighboursBinary += "1";
                }
                else
                {
                    neighboursBinary += "0";
                }
                tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinary);

            }
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinary = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinary += "1";
                }
                else
                {
                    neighboursBinary += "0";
                }
            }
            tilemapVisualizer.paintSingleBasicWall(position, neighboursBinary);
        }
    }

    private static HashSet<Vector2Int> FindwallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);

            }
        }
        return wallPositions;
    }
}
