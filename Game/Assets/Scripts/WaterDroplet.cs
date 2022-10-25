using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterDroplet: MonoBehaviour
{
    public float fillAmount = 15; //how much it refills water bar out of 100
    [SerializeField] private float reappearDelay = 10; //makes the droplet reappear after this many seconds
	private GameObject dropletFX; //child with renderer and collider(gets set inactive when collected)

    private void Start()
    {
        dropletFX = GetComponentInChildren<SpriteRenderer>().gameObject;
    }

    void Update()
    {
        if (dropletFX.activeSelf == false)
        {
     	    StartCoroutine(ReappearDelay());
     	}   
    }
    IEnumerator ReappearDelay()
    {
    	yield return new WaitForSeconds(reappearDelay);
    	dropletFX.SetActive(true);
        StopAllCoroutines();
    }
}
