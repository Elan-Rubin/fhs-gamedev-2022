using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidBody;

    public float raycastdistance;

    public float movespeed;

    private bool canswitchdirection = true;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(movespeed, rigidBody.velocity.y);//moves player right 
        RaycastHit2D detectionraycast = Physics2D.Raycast(transform.position, Vector2.right + Vector2.down, raycastdistance); //shoots a raycast right and down
        RaycastHit2D detectionraycast2 = Physics2D.Raycast(transform.position, Vector2.left + Vector2.down, raycastdistance); //shoots a raycast left and down
        Debug.Log(detectionraycast.collider);
        if ((detectionraycast.collider == null ||detectionraycast2.collider==null) && canswitchdirection)
        {
            StartCoroutine(delayswitch());
            movespeed = movespeed * -1;
        }
    }
    IEnumerator delayswitch()
    {
        canswitchdirection = false;
       
        yield return new WaitForSeconds(1f);
        canswitchdirection = true;
    }
}
