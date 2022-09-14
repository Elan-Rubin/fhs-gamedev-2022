using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackrate = 1.2f;
    public float attackradius = 1f;
    // How long it takes to attack
    private float attacksSec;

    private float timeLastAttacked;
    // Start is called before the first frame update
    void Start()
    {
        attacksSec = 1 / attackrate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && timeLastAttacked + attacksSec <= Time.time)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackradius);
            foreach (Collider2D hit in hits)
            {
                Debug.Log(hit.gameObject.name);
            }
            timeLastAttacked = Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackradius);
    }
}
