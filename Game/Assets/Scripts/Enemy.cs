using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private bool canSwitchDirection = true;
    [Header("Collision")] //for some reason this header isn't showing up in the editor? is this just on my end? 
    private Rigidbody2D rigidBody;
    [SerializeField] float groundRaycastDistance;
    [SerializeField] float wallRaycastDistance;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);//moves enemy right
        RaycastHit2D groundRaycastR = Physics2D.Raycast(transform.position, Vector2.right + Vector2.down, groundRaycastDistance); //shoots a raycast right and down
        RaycastHit2D groundRaycastL = Physics2D.Raycast(transform.position, Vector2.left + Vector2.down, groundRaycastDistance); //shoots a raycast left and down
        RaycastHit2D wallRaycastR = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastDistance); //shoots a raycast right
        RaycastHit2D wallRaycastL = Physics2D.Raycast(transform.position, Vector2.left, wallRaycastDistance); //shoots a raycast left
        //Debug.Log(detectionraycast.collider);
        if (((groundRaycastR.collider == null || groundRaycastL.collider == null) ||
            (wallRaycastR.collider != null || wallRaycastL.collider != null)) && canSwitchDirection)
        {
            StartCoroutine(DelaySwitch());
            movementSpeed *= -1;
        }
    }
    IEnumerator DelaySwitch()
    {
        canSwitchDirection = false;
        yield return new WaitForSeconds(1f);
        canSwitchDirection = true;
    }
}
