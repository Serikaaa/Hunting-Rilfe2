using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 1;
    PlayerHealth playerHealth;
    private void nCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerHealth>();
        if (player)
        {
            player.TakeDamage(damage);
        }
    }
}
