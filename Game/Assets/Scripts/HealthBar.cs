using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    
    // Start is called before the first frame update
    void Start()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue); // Make sure it starts green, not red
    }
    
    public void updateBar(Damageable tracking) {
        // update the health bar
        slider.maxValue = tracking.maxHealth;
        slider.value = tracking.Health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
