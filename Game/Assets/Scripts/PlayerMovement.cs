using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5;
    public float jumpHeight = 5;
    Rigidbody2D rigidBody;
    public float jumpdistance = 1;
    bool canJump = true;

    [SerializeField] private LayerMask ground; // editor ground layer
    private int groundLayer; // log base 2 of ground (^) actually used

    [SerializeField] private float wallJumpDist = 1f; // Distance from side of player
    [SerializeField] private float wallJumpDistSide = 2f; // Side power of wall jump
    private float wallJumpSideDistNow = 0f;
    private bool inAir;
    void Start() {
        groundLayer = (int)Mathf.Log(ground, 2);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        wallJumpSideDistNow = 0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,jumpdistance);
        if (hit.collider == null) {
            inAir = true;
        }   
        else {
            inAir = false;
        }
        // Debug.Log(hit.collider);
        if (UnityEngine.Input.GetAxisRaw("Vertical")>0.1f && hit.collider!=null && canJump == true)
        {
            StartCoroutine(JumpDelay());
            rigidBody.velocity += Vector2.up * jumpHeight;
        }

        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, wallJumpDist, ground);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, wallJumpDist, ground);
        
        

        if ((left.collider != null || right.collider != null) && /*Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 &&*/ Input.GetButtonDown("Vertical") && canJump) {
            StartCoroutine(JumpDelay());
            rigidBody.velocity += Vector2.up * jumpHeight;
            wallJumpSideDistNow = left.collider != null ? (wallJumpDistSide) : (wallJumpDistSide * -1);
            // Debug.Log(wallJumpSideDistNow);
        }

        rigidBody.velocity = new Vector2(runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal") + wallJumpSideDistNow, rigidBody.velocity.y);
        //Debug.Log(rigidBody.velocity.x + "jhifsd");
        //rigidBody.velocity += Vector2.right * runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal");
    }

    IEnumerator JumpDelay()
    {
        canJump = false;
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, wallJumpDist);
        Gizmos.DrawWireSphere(transform.position, jumpdistance);
    }
}
