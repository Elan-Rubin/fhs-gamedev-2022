using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRate = 1.2f;
    public float attackRadius = 1f;
    public LayerMask enemyLayer;
    // How long it takes to attack
    private float attacksSec;
    public int damage = 13;

    private float timeLastAttacked;
    void Start()
    {
        attacksSec = 1 / attackRate;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && timeLastAttacked + attacksSec <= Time.time)
        {
            // attack animation when available
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.layer == Mathf.Log(enemyLayer, 2)) // find out if hit an enemy
                {
                    hit.GetComponent<Damageable>().TakeDamage(damage);
                    // Debug.Log("hit");
                }
            }
            timeLastAttacked = Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
