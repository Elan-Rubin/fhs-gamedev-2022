using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    [SerializeField] private float runSpeed = 5;
    //[SerializeField] private float baseRunSpeed = 3;
    //[SerializeField] private float sprintSpeedMultiplier = 2f;

    [Header("Dashing")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPowerMultiplier = 1f;
    [SerializeField] private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;
    [SerializeField] TrailRenderer dashTrail;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float groundedDist = 1;
    [SerializeField] private float maxVertSpeed = 20;
    private bool canJump = true;
    private bool isGrounded = false;
    [Header("Walljumping")]
    [Tooltip("Distance from side of player")] [SerializeField] private float wallJumpDist = 1f;
    [Tooltip("Side power of walljump")][SerializeField] private float wallJumpDistSide = 2f;
    private float wallJumpSideDistNow = 0f;
    private float sidewaysMomentum = 1f;
    private float wallJumpTime;
    [Header("Collision")]
    [Tooltip("Editor ground layer")][SerializeField] private LayerMask ground;
    private int groundLayer; // log base 2 of ground (^) actually used
    private Rigidbody2D rigidBody;
    private WaterLevel waterLevel;


    [SerializeField] private Transform camera;
    void Start()
    {
        groundLayer = (int)Mathf.Log(ground, 2);
        rigidBody = GetComponent<Rigidbody2D>();
        waterLevel = GetComponent<WaterLevel>();
        //runSpeed = baseRunSpeed;
    }

    void Update()
    {
        GroundJump();
        WallJump();

        // sprinting
        //runSpeed = baseRunSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeedMultiplier : 1);

        // Dashing
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());

        //set velocity of rigidbody based on horizontal input, add the wall jump momentum, clamp the vertical speed for falling and wall accelerating upward
        rigidBody.velocity = new Vector2((runSpeed * Input.GetAxisRaw("Horizontal")) + wallJumpSideDistNow, Mathf.Clamp(rigidBody.velocity.y, -maxVertSpeed, maxVertSpeed));
    }                                                                                                                                                                       

    private void GroundJump()
    {
        RaycastHit2D groundedRaycast = Physics2D.Raycast(transform.position, -Vector2.up, groundedDist);//grounded raycast to detect if on the ground
        isGrounded = groundedRaycast.collider != null;
        //Debug.Log($"Canjump: {canJump} Raycast: {groundedRaycast.collider != null}");
        if (((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) || Input.GetKeyDown(KeyCode.Space)) && isGrounded && canJump == true) //if vertical input, is grounded, and doesn't have jump cooldown
        {
            StartCoroutine(JumpDelay());//Small cooldown for jump
            rigidBody.velocity += Vector2.up * jumpHeight;
            // camera.GetComponent<CameraShake>().cameraShake();
        }
    }

    private void WallJump()
    {
        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, wallJumpDist, ground);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, wallJumpDist, ground);

        if ((left.collider != null || right.collider != null) && /*Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 &&*/ (Input.GetButtonDown("Vertical") || Input.GetKeyDown(KeyCode.Space)) && canJump)
        {
            StartCoroutine(JumpDelay());
            //rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0); //
            rigidBody.velocity = Vector2.up * jumpHeight;
            wallJumpSideDistNow = left.collider != null ? (wallJumpDistSide) : (wallJumpDistSide * -1);
            wallJumpTime = Time.time;
            //Debug.Log(wallJumpSideDistNow);
        }

        float sidePower = Mathf.Exp(-4 * (Time.time - wallJumpTime));
        if (sidePower < 0.2)
            sidePower = 0;
        if (isGrounded) //TODO make this fix better for jumping up the same wall instead of back and forth between two
            sidePower = 0;
        if (wallJumpSideDistNow > 0)
            wallJumpSideDistNow = wallJumpDistSide * sidePower;
        if (wallJumpSideDistNow < 0)
            wallJumpSideDistNow = wallJumpDistSide * sidePower * -1;
    }

    IEnumerator Dash()//sets gravity to 0, turns on dash trail, adds horizontal velocity in the same way as wall jump
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        wallJumpSideDistNow = rigidBody.velocity.x > 0 ? (wallJumpDistSide) * dashPowerMultiplier : (dashPowerMultiplier * wallJumpDistSide * -1);
        wallJumpTime = Time.time;
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        dashTrail.emitting = false;
        rigidBody.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator JumpDelay()//small cooldown for jump
    {
        canJump = false;
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, wallJumpDist);
        //Gizmos.DrawWireSphere(transform.position, jumpDistance); 
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallJumpDist, transform.position.y)); //I've never done gizmos before but I made them
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - wallJumpDist, transform.position.y)); //lines instead of circles I thought it might be good
        Gizmos.DrawLine(transform.position, -Vector2.up * groundedDist);
    }
}
