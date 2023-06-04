using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class EnemyHealth : NetworkBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 10;
    private int health;
    private void Start()
    {
        health = maxHealth;
    }
    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
