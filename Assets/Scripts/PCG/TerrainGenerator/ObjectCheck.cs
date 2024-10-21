using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    //RaycastHit hit;
    private void Start()
    {
        /*if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10f))
        {
            Debug.Log("GROUNDED");
            //this.transform.position = hit.point;

            //this.transform.Translate(hit.point);
            //this.transform.position = hit.point * -Vector3.up * 1;
        }*/

        Collider collider = gameObject.GetComponent<Collider>();

        Vector3 lowestPoint = collider.bounds.min;
        RaycastHit hit;
        if (Physics.Raycast(lowestPoint + Vector3.up * 0.1f, Vector3.down, out hit))
        {

            float distanceToMoveDown = Vector3.Distance(lowestPoint, hit.point);
            gameObject.transform.position -= Vector3.up * distanceToMoveDown;
            Debug.Log("meeep");

            /* else
             {
                 Debug.Log("Deleting object");
                 Destroy(this.gameObject);
             }*/
        }
    }
}
