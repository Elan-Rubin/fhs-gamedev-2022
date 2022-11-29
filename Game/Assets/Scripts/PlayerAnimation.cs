using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    [SerializeField] private List<Sprite> idleSprites;
    private int idleNum;
    [SerializeField] private SpriteRenderer rend;
    private Rigidbody2D rigidBody;

    [SerializeField] private float period = 1f;
    private float lastChanged;

    private animationState state = animationState.idle;
    public enum animationState {
        idle
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Time.time - lastChanged) >= period)
        {
            if (idleNum < idleSprites.Count) {
                rend.sprite = idleSprites[idleNum];
                idleNum++;
            }
            else if (idleNum >= idleSprites.Count) {
                idleNum = 0;
                rend.sprite = idleSprites[idleNum];
            }

            lastChanged = Time.time;
        }

        if (rigidBody.velocity.x > 0)//simple flip sprite based on velocity(to make it look the direction of movement)
            rend.flipX = false;
        else if (rigidBody.velocity.x < 0)
            rend.flipX = true;
    }
}
