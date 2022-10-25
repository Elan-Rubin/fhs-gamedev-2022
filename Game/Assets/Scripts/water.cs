using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class water : MonoBehaviour
{
	public GameObject waters;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (waters.activeSelf == false) {
     		StartCoroutine(ReappearDelay());
     	}   
    }
    IEnumerator ReappearDelay()
    {
    	yield return new WaitForSeconds(15);
    	waters.SetActive(true);
    }
}
