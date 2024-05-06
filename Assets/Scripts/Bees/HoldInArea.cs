using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoldInArea : Behaviour
{

    float maxRange;

    private Vector3 myAim;
    private bool checkOutOfBounds = true;

    bool returning = false;

    private BeeBoid myBee;
    private void Awake()
    {
        myBee = GetComponent<BeeBoid>();

        maxRange = FindObjectOfType<BoxGenerator>().scale;
        maxRange *= myBee.beeStats[2].statBase;

    }

    public override Vector3 CalculateForce()
    {

        Vector3 calForce = new();

        if (checkOutOfBounds && !returning && DistanceOut())
        {

            myAim = myBee.currentBox.transform.parent.position;
            myAim.y = transform.position.y;

            returning = true;

        }

        if(returning)
        {
            float dis = Vector3.Distance(myAim, transform.position);

            GetComponent<Flock>().active = false;

            if (dis < 1f)
            {
                myAim = new();
                returning = false;
                GetComponent<Flock>().active = true;
            }
            else
                calForce = (myAim - transform.position) * weight;
        }

        return calForce;

    }

    private bool DistanceOut()
    {

        Vector3 otherLoc = myBee.currentBox.transform.position;

        Vector2 vec1 = new Vector2(transform.position.x, transform.position.z);
        Vector2 vec2 = new Vector2(otherLoc.x, otherLoc.z);

        bool check = Vector2.Distance(vec1, vec2) > maxRange;

        return check;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(returning && other.tag == "Box")
        {
            returning = false;
            GetComponent<Flock>().active = true;
        }

    }

}
