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
    [SerializeField] float raycastDistance;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);//moves player right 
        RaycastHit2D detectionraycast = Physics2D.Raycast(transform.position, Vector2.right + Vector2.down, raycastDistance); //shoots a raycast right and down
        RaycastHit2D detectionraycast2 = Physics2D.Raycast(transform.position, Vector2.left + Vector2.down, raycastDistance); //shoots a raycast left and down
        //Debug.Log(detectionraycast.collider);
        if ((detectionraycast.collider == null || detectionraycast2.collider == null) && canSwitchDirection)
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
