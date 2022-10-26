using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    [SerializeField] private List<Sprite> idleSprites;
    private int idleNum;
    [SerializeField] private SpriteRenderer rend;

    [SerializeField] private float period = 1f;
    private float lastChanged;

    private animationState state = animationState.idle;
    public enum animationState {
        idle
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    }
}
