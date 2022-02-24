using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldHandle : MonoBehaviour
{

    public CorridorFirstDungeonGeneration dungeon;
    private List<Vector2Int> floorspace;
    private Vector2 startPos2;
    private Vector3 startPos;
    public List<GameObject> enemys;
    private bool moreLeft = true;

    public int totalBaddies = 0;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        dungeon.GenerateDungeon();

        startPos2 = dungeon.ReturnFloorSpace();
        startPos = startPos2;
        player.transform.position = startPos;

        foreach (var enemy in enemys)
        {
            startPos2 = dungeon.ReturnFloorSpace();
            startPos = startPos2;
            enemy.transform.position = startPos;
        }



    }

    void Update()
    { 
        if (moreLeft)
        {
            totalBaddies = CheckEnemies(enemys);
            if (totalBaddies == 12)
            {
                //create Key
                moreLeft = false;
                Debug.Log("Winner");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

    }

    private int CheckEnemies(List<GameObject> enemys)
    {
        int count = 0;
        foreach(var enemy in enemys)
        {
            if (enemy == null)
            {
                count++;

            }
        }
        return count;
    }
}
