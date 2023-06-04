using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Enemy2 : NetworkBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;

    public Transform firingPoint1;
    public Transform firingPoint2;

    public float fireRate;
    private float timeToFire;

    public float bulletForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target) {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
        if(target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }
        
    }
    private void Shoot()
    {
        if(timeToFire <= 0f)
        {
            Debug.Log("Shoot");
            GameObject bullet1 = Instantiate(bulletPrefab, firingPoint1.position, firingPoint1.rotation);
            GameObject bullet2 = Instantiate(bulletPrefab, firingPoint2.position, firingPoint2.rotation);
            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb1.AddForce(firingPoint1.up * bulletForce, ForceMode2D.Impulse);
            rb2.AddForce(firingPoint2.up * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet1, 5f);
            Destroy(bullet2, 5f);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if(target != null)
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
            {
                rb.velocity = transform.up * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
       
        //Move forward
        
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
        
    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
       
    }

}
