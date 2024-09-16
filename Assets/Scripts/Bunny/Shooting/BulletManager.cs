using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [Header("Scripteble Objects")]
    public WeaponStatistics weaponStatistics;


    [Header("Bullet Position")]

     private Vector3 startPosition;
     private float distanceTraveled;


    private void Start()
    {
        startPosition = transform.position;
    }


    private void Update()
    {
        Range();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            gameObject.GetComponent<Health>().TakeDamage(weaponStatistics.damage);
        }
    }


    ///Controlls the range of the bullet
    private void Range()
    {
        distanceTraveled = Vector3.Distance(transform.position, startPosition);


        if (distanceTraveled > weaponStatistics.range)
        {
            Destroy(gameObject);
        }
    }
}
