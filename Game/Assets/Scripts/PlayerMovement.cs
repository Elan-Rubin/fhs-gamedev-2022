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
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,jumpdistance);
        Debug.Log(hit.collider);
        if (UnityEngine.Input.GetAxisRaw("Vertical")>0.1f && hit.collider!=null && canJump == true)
        {
            StartCoroutine(JumpDelay());
            rigidBody.velocity += Vector2.up * jumpHeight;
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
}
