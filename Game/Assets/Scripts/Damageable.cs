using UnityEngine;

/// <summary>
/// Represents something that can be damaged. Keeps track of <see cref="Health"/>
/// </summary>?
public class Damageable : MonoBehaviour
{
    [SerializeField]
    private int _health; // backing field for Health (can mostly be ignored)
    public HealthBar bar; // health bar, if it exists
    public int startingHealth;

    void Start()
    {
        _health = startingHealth;
    }

    public bool takeDamage = false; //TODO: Temporary for testing

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
        private set // don't let others modify healtpublic HealGameObject prefab;h directly, but anyone can read it
        {
            _health = Mathf.Clamp(value, 0, maxHealth); // clamp health within 0 and max
        }
    }

   

    /// <summary>
    /// The maximum health this can hold
    /// </summary>
    public int maxHealth { get; private set; } // don't allow others to set this


    void Awake() // TODO: Temporary for testing
    {
        maxHealth = 20;
        Health = maxHealth;
    }
    
    void Update() // TODO: Temporary for testing
    {
        if (takeDamage)
        {
            TakeDamage(1);
            takeDamage = false;
        }
    }
    
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

        // update the health bar, if it exists
        if (bar != null) bar.updateBar(this);

        return oldHealth - Health;
    }
}