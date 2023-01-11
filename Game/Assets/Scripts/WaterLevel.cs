using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevel : MonoBehaviour
{
    public float waterAmount = 100;
    public float dryingRate = 1;
    public float iFrameTime = 3f;
    [SerializeField] Slider healthBarSlider;
    [SerializeField] Image indicatorUi;
    [SerializeField] List<Sprite> dropletSprites;
    [SerializeField] ParticleSystem splashParticles;
    private float maxWaterAmount;
    private bool isInLake = false;
    private bool isInvincible = false; //I-frames

    private void Start()
    {
        maxWaterAmount = waterAmount;
    }

    private void Update()
    {
        if (waterAmount >= 0)
            waterAmount -= dryingRate * Time.deltaTime; //Decrease water level by drying rate
        healthBarSlider.value = waterAmount / maxWaterAmount;
        try
        {
            indicatorUi.sprite = dropletSprites[dropletSprites.Count - (int)((waterAmount / maxWaterAmount) * dropletSprites.Count) - 1];
        }
        catch { }
        if (isInLake)
        {
            waterAmount += Time.deltaTime * 30f;
            if (waterAmount > 100) //can't add more than 100% water level
                waterAmount = 100;
        }
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
        if (collision.GetComponent<Lake>() != null)
        {
            isInLake = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Lake>() != null)
        {
            isInLake = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            DecreaseWaterLevel(10);
        }
        if (collision.tag.Equals("Respawn"))
        {
            waterAmount = 0;
        }
    }

    public void DecreaseWaterLevel(float damage)
    {
        if (isInvincible == false)
        {
            StartCoroutine(iFrameDelay());
            waterAmount -= damage;
            splashParticles.Emit(5);
        }
    }

    IEnumerator iFrameDelay()
    {
        isInvincible = true;
        yield return new WaitForSeconds(iFrameTime);
        isInvincible = false;
    }
}
