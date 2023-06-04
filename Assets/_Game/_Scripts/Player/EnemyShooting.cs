using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class EnemyShooting : NetworkBehaviour
{
    public Transform firePoint1;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetButtonDown("Fire1") && Time.time >nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);
        bullet1.GetComponent<NetworkObject>().Spawn();
        Destroy(bullet1, 0.5f);
    }


}
