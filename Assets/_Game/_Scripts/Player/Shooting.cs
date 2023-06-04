using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Shooting : NetworkBehaviour
{
    [SerializeField] public Transform firePoint1;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private List<GameObject> spawnedbullet = new List<GameObject>();
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
            ShootServerRpc();
        }
    }
    [ServerRpc]
    private void ShootServerRpc()
    {
        ShootClientRpc();
    }
    [ClientRpc]
    private void ShootClientRpc()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);
    }

    [ServerRpc(RequireOwnership =false)]
    public void DestroyServerRpc()
    {
        GameObject toDestroy = spawnedbullet[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedbullet.Remove(toDestroy);
        Destroy(toDestroy);
    }
}
