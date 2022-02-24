using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeCDAttack;
    public float attackCD;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public float attackDmg = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeCDAttack <= 0)
        {
            //can attack
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(attackDmg);
                }
            }
            timeCDAttack = attackCD;
        }
        else
        {
            timeCDAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }
}
