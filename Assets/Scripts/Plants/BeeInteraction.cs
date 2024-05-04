using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeeInteraction : MonoBehaviour
{

    public bool takenByBee = false;

    public Transform targetPoint;

    private void Start()
    {

        Box[] boxes = FindObjectsOfType<Box>();

        Box nearestBox = null;
        float boxDis = 0;

        foreach (Box box in boxes)
        {

            if(nearestBox == null)
            {
                nearestBox = box;
                boxDis = Vector3.Distance(box.transform.position, transform.position);

            }
            else if(boxDis >= Vector3.Distance(box.transform.position, transform.position))
            {
                nearestBox = box;

                boxDis = Vector3.Distance(box.transform.position, transform.position);
            }
        }
        if (nearestBox != null)
        {
            nearestBox.objectsInBox.Add(transform);
        }

    }

}