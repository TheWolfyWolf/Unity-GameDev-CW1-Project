using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CorridorFirstDungeonGeneration dungeon;
    private List<Vector2Int> floorspace;
    private Vector2 startPos2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getPos()
    {
        return transform.position;
    }

    public List<Vector2Int> getFloorSpace()
    {
        return floorspace;
    }
}
