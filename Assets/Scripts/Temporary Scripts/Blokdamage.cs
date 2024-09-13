using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blokdamage : MonoBehaviour
{
    

    public float damageAmmount = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BunnyStats>().TakeDamage(damageAmmount);
        }
    }
}
