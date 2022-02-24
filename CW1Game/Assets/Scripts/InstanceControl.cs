using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceControl : MonoBehaviour
{
    public CorridorFirstDungeonGeneration dungeon;
    private List<Vector2Int> floorspace;
    private Vector2 startPos2;

    public Player player;
    public Rigidbody2D rb;
    public float speed = 1f;
    public float attackRange = 2f;

    public float health = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.getPos()) < attackRange)
        {
            //target player
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.getPos(), step);
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    internal void TakeDamage(float attackDmg)
    {
        health -= attackDmg;
    }
}
