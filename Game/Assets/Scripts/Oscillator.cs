using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    /* This script can be used for making item pickups, characters, etc. move
     * up and down along a sine wave. It should also be robust enough to make
     * moving platforms which go back and forth and stuff
     */
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    [SerializeField] bool oscillateScale = false;

    // todo remove from inspector later
    [Range(0,1)] [SerializeField] float movementFactor; //0 for not moved, 1 for fully moved

    Vector3 startingPos;
    Vector3 startingScale;

	void Start () {
		startingPos = transform.position;
        startingScale = transform.localScale;
    }
	
	void Update () {
        //set movement factor
        //todo protect against period is zero
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2f; //about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        if (!oscillateScale)
        {
            transform.position = startingPos + offset;
        }
        if (oscillateScale)
        {
            transform.localScale = startingScale + offset;
        }
	}
}
