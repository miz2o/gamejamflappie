using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    RaycastHit hit;
    Vector3 hitPoint;
    private void Start()
    {
        hitPoint = new Vector3(0,0.95f,0);
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10f))
        {
           // hitPoint = hit.point;
            Debug.Log("GROUNDED");
            //this.transform.position = hit.point * hitPoint;
        }

       /* else
        {
            Debug.Log("Deleting object");
            Destroy(this.gameObject);
        }*/
    }
}
