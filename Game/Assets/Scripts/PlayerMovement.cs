using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5;
    public float jumpHeight = 5;
    Rigidbody2D rigidBody;
    public float jumpdistance = 1;//distance from player to ground for grounded
    bool canJump = true;//manages small jump cooldown
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D groundedRaycast = Physics2D.Raycast(transform.position, -Vector2.up,jumpdistance);//grounded raycast to detect if on the ground TODO differentiate between player and ground so it doesn't hit player
        if ((UnityEngine.Input.GetAxisRaw("Vertical")>0.1f || Input.GetKeyDown(KeyCode.Space))
            && groundedRaycast.collider!=null && canJump == true) //if vertical input, is grounded, and doesn't have jump cooldown
        {
            StartCoroutine(JumpDelay());//Small cooldown for jump
            rigidBody.velocity += Vector2.up * jumpHeight;
        }
        rigidBody.velocity = new Vector2(runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);//set velocity of rigidbody based 
    }                                                                                                                 //on horizontal input
    private void OnTriggerEnter2D(Collider2D collision)//simple respawn system TODO:move to it's own script
    {
        if (collision.tag == "Respawn"){
            transform.position = new Vector2(0, 0);
        }
    }

    IEnumerator JumpDelay()//small cooldown for jump
    {
        canJump = false;
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }
}
