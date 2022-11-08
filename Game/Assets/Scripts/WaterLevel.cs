using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevel : MonoBehaviour
{
    public float waterAmount = 100;
    public float dryingRate = 1;
    [SerializeField] Slider healthBarSlider;
    [SerializeField] Image indicatorUi;
    [SerializeField] List<Sprite> dropletSprites;
    private float maxWaterAmount;

    private void Start()
    {
        maxWaterAmount = waterAmount;
    }

    private void Update()
    {
        if (waterAmount >= 0)
            waterAmount -= dryingRate * Time.deltaTime; //Decrease water level by drying rate
        healthBarSlider.value = waterAmount / maxWaterAmount;
        indicatorUi.sprite = dropletSprites[dropletSprites.Count - (int)((waterAmount/maxWaterAmount) * dropletSprites.Count) - 1];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<WaterDroplet>() != null) //Check if the collider's parent has WaterDroplet script(droplet detection)
        {
            WaterDroplet dropletScript = collision.GetComponentInParent<WaterDroplet>();
            waterAmount += dropletScript.fillAmount;//increse water level by droplet's fillamount
            collision.gameObject.SetActive(false); //set just FX and collider of droplet inactive(not parent w/ script_
            if (waterAmount > 100) //can't add more than 100% water level
            {
                waterAmount = 100;
            }
        }
    }
}
