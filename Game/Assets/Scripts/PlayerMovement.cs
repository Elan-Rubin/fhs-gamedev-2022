using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5;
    public float jumpHeight = 5;
    Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }



    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.velocity += Vector2.up*jumpHeight;
        }
        //rigidBody.velocity = new Vector2(rigidBody.velocity.y, runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal"));
        rigidBody.velocity += Vector2.right * runSpeed * UnityEngine.Input.GetAxisRaw("Horizontal");
    }
}
