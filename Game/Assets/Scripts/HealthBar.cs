using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls a health bar
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    
    /// <summary>
    /// Update this bar
    /// </summary>
    /// <param name="tracking">The damageable to represent</param>
    public void UpdateBar(Damageable tracking) {
        slider.maxValue = tracking.maxHealth;
        slider.value = tracking.Health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
