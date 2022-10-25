using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevel : MonoBehaviour
{
    public float waterAmount = 100;
    public float dryingRate = 1;
    [SerializeField] Slider healthBarSlider;
    private float maxWaterAmount;

    private void Start()
    {
        maxWaterAmount = waterAmount;
    }

    private void Update()
    {
        waterAmount -= dryingRate * Time.deltaTime;
        healthBarSlider.value = waterAmount / maxWaterAmount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="waterdrop")
        {
            waterAmount += 15;
            collision.gameObject.SetActive(false);
            if (waterAmount > 100) 
            {
                waterAmount = 100;
            }

        }
    }
}
