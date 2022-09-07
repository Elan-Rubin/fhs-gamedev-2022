using UnityEngine;

/// <summary>
/// Represents something that can be damaged. Keeps track of <see cref="Health"/>
/// </summary>
public class Damageable : MonoBehaviour
{
    private int _health; // backing field for Health (can mostly be ignored)

    /// <summary>
    /// The current amount of health
    /// Automatically clamped between 0 and <see cref="maxHealth"/>
    /// </summary>
    public int Health
    {
        get
        {
            return _health;
        }
        private set // don't let others modify health directly, but anyone can read it
        {
            _health = Mathf.Clamp(value, 0, maxHealth); // clamp health within 0 and max
        }
    }

    /// <summary>
    /// The maximum health this can hold
    /// </summary>
    public int maxHealth;

    
    /// <summary>
    /// Attempts to take damage
    /// </summary>
    /// <param name="damage">The damage to take</param>
    /// <returns>the actual damage taken</returns>
    public int TakeDamage(int damage)
    {
        var oldHealth = Health;
        // this is where stuff like armor and buffs might go?
        Health -= damage;
        return oldHealth - Health;
    }
}