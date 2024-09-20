using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
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
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(weaponStatistics.damage);
            }
        }


        if (collision.gameObject.layer == 8)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<AIHealth>().TakeDamage(weaponStatistics.damage);
            }


            Destroy(gameObject);
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
