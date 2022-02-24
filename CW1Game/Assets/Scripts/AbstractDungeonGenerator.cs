using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public List<Vector2Int> floorspace;

    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        floorspace = RunProceduralGeneration();
    }

    public Vector2Int ReturnFloorSpace()
    {
        return floorspace[UnityEngine.Random.Range(0, floorspace.Count)];
    }

    protected abstract List<Vector2Int> RunProceduralGeneration();

}
