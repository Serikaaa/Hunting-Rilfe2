using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class EnemyBullet : NetworkBehaviour
{
    public Shooting parent;
    [SerializeField] public GameObject hitEffect;
    public int damage = 1;
    public PlayerHealth playerHealth;
    /*  private void OnCollisionEnter2D(Collision2D collision)
      {
          if (!IsOwner) return;
          GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
          Destroy(effect, 0.2f);
          Destroy(gameObject);
      }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);
        var player = collision.collider.GetComponent<PlayerHealth>();
        if (player)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
        
    }

}

