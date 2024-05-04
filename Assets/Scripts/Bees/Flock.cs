using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flock : Behaviour
{

    [SerializeField] float minimumDistance = 1f;

    private List<BeeBoid> myBois = new();

    

    /*[SerializeField] float goldenRatio = 2f;

    private Box currentBox;

    public override Vector3 CalculateForce()
    {

        Vector3 calForce = new Vector3();

        currentBox = GetComponent<BeeBoid>().currentBox;

        foreach (Transform bee in currentBox.objectsInBox)
        {
            if (bee.tag == gameObject.tag && bee != transform)
            {

                float distance = Vector3.Distance(transform.position, bee.position);

                float dirMod = distance - goldenRatio;

                calForce += (bee.position - transform.position) * dirMod;

                if(calForce.magnitude > weight)
                {
                    calForce = Vector3.ClampMagnitude(calForce, weight);
                    break;
                }

            }
        }

        if (calForce.magnitude < weight)
        {
            foreach (Box box in currentBox.neighbours)
            {
                foreach (Transform bee in box.objectsInBox)
                {
                    if (bee.tag == gameObject.tag && bee != transform)
                    {

                        float distance = Vector3.Distance(transform.position, bee.position);

                        float dirMod = distance - goldenRatio;

                        calForce += (bee.position - transform.position) * dirMod;

                    }

                    if (calForce.magnitude > weight)
                    {
                        calForce = Vector3.ClampMagnitude(calForce, weight);
                        break;
                    }

                }
            }
        }

        return calForce;
    }
    */
}
