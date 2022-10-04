using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5;
    public float jumpHeight = 5;
    Rigidbody2D rigidBody;
    public float jumpdistance = 1;
    bool canJump = true;
    public int baseJumpsRemaining = 2;
    public int jumpsRemaining;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,jumpdistance);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            jumpsRemaining = baseJumpsRemaining;
        }
        if (UnityEngine.Input.GetAxisRaw("Vertical")>0.1f && canJump == true && jumpsRemaining > 0)
        {
            jumpsRemaining --;
            StartCoroutine(JumpDelay());
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
            //rigidBody.velocity += Vector2.up * jumpHeight;
        }
        rigidBody.velocity = new Vector2(runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);
        //rigidBody.velocity += Vector2.right * runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal");
    }

    IEnumerator JumpDelay()
    {
        canJump = false;
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
    // jumpsRemaining++;
        Destroy(col.gameObject);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
    }
}
