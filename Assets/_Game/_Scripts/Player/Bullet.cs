using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Bullet : NetworkBehaviour
{
    [SerializeField] public GameObject hitEffect;
    private EnemyHealth enemyHealth;
    public int damage;


     void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);
        var enemy = collision.collider.GetComponent<EnemyHealth>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
        
        Destroy(gameObject);

    }

}

