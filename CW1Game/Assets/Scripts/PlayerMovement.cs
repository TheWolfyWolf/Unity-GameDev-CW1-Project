using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Vector2 playerMovement;

    public Rigidbody2D rb;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");
        Vector3 localscale = transform.localScale;
        animator.SetFloat("Horizontal", playerMovement.x);
        animator.SetFloat("Verical", playerMovement.y);
        animator.SetFloat("Speed", playerMovement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Attack", true);
        }

        if (playerMovement.x == -1)
        {
            localscale.x = -1;
        }
        else
        {
            localscale.x = 1;
        }
        transform.localScale = localscale;
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerMovement * moveSpeed * Time.fixedDeltaTime);
    }
}
