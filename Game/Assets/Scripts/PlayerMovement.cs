using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking/Sprinting")]
    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float baseRunSpeed = 3;
    [SerializeField] private float sprintSpeedMultiplier = 2f;
    [Header("Jumping")]
    //private bool inAir; // is this variable used?
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float jumpDistance = 1;
    private bool canJump = true;
    [Header("Walljumping")]
    [Tooltip("Distance from side of player")] [SerializeField] private float wallJumpDist = 1f;
    [Tooltip("Side power of walljump")][SerializeField] private float wallJumpDistSide = 2f;
    private float wallJumpSideDistNow = 0f;
    [Header("Collision")]
    [Tooltip("Editor ground layer")][SerializeField] private LayerMask ground;
    private int groundLayer; // log base 2 of ground (^) actually used
    private Rigidbody2D rigidBody;
    void Start()
    {
        groundLayer = (int)Mathf.Log(ground, 2);
        rigidBody = GetComponent<Rigidbody2D>();
        runSpeed = baseRunSpeed;
    }

    void Update()
    {
        wallJumpSideDistNow = 0f;
        RaycastHit2D groundedRaycast = Physics2D.Raycast(transform.position, -Vector2.up, jumpDistance);//grounded raycast to detect if on the ground
        //Debug.Log($"Canjump: {canJump} Raycast: {groundedRaycast.collider != null}");
        if ((Input.GetButtonDown("Vertical") || Input.GetKeyDown(KeyCode.Space))
            && groundedRaycast.collider != null && canJump == true) //if vertical input, is grounded, and doesn't have jump cooldown
        {
            StartCoroutine(JumpDelay());//Small cooldown for jump
            rigidBody.velocity += Vector2.up * jumpHeight;
        }
        // sprinting
        runSpeed = baseRunSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeedMultiplier : 1);

        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, wallJumpDist, ground);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, wallJumpDist, ground);



        if ((left.collider != null || right.collider != null) && /*Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 &&*/ (Input.GetButtonDown("Vertical") || Input.GetKeyDown(KeyCode.Space)) && canJump)
        {
            StartCoroutine(JumpDelay());
            rigidBody.velocity += Vector2.up * jumpHeight;
            wallJumpSideDistNow = left.collider != null ? (wallJumpDistSide) : (wallJumpDistSide * -1);
            // Debug.Log(wallJumpSideDistNow);
        }
        rigidBody.velocity = new Vector2(runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal") + wallJumpSideDistNow, rigidBody.velocity.y);//set velocity of rigidbody based 
    }                                                                                                                 //on horizontal input
    private void OnTriggerEnter2D(Collider2D collision)//simple respawn system TODO:move to it's own script
    {
        if (collision.tag.Equals("Respawn"))
        {
            transform.position = new Vector2(0, 0);
        }
    }

    IEnumerator JumpDelay()//small cooldown for jump
    {
        canJump = false;
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, wallJumpDist);
        Gizmos.DrawWireSphere(transform.position, jumpDistance);
    }
}
