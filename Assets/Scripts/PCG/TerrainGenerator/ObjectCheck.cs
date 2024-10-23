using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    RaycastHit hit;
    private void Start()
    {
        Invoke("MoveObjects", 1);
    }

    void MoveObjects()
    {
        Collider collider = gameObject.GetComponent<Collider>();

        Vector3 lowestPoint = collider.bounds.min;
        if (Physics.Raycast(lowestPoint + Vector3.up * 0.1f, Vector3.down, out hit))
        {
            float distanceToMoveDown = Vector3.Distance(lowestPoint, hit.point);
            gameObject.transform.position -= Vector3.up * distanceToMoveDown;
        }
    }
}
