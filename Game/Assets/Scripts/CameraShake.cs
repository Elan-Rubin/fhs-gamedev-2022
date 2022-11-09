using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakePower = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cameraShake() {
        float xshake = Random.Range(0f, 1f);
        float yshake = Random.Range(0f, 1f);
        Vector2 shake = new Vector2(xshake, yshake) * shakePower;
        transform.Translate(shake);
    }
}
