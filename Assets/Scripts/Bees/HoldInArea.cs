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

    [SerializeField]private BeeBoid myBee;
    private void Start()
    {


        myBee = GetComponent<BeeBoid>();
        maxRange *= myBee.beeStats[2].statBase;

        maxRange = FindObjectOfType<BoxGenerator>().scale;

    }

    public override Vector3 CalculateForce()
    {

        Vector3 calForce = new();

        if (checkOutOfBounds && !returning && DistanceOut())
        {

            myAim = myBee.currentBox.neighbours[0].transform.position;
            myAim.y = transform.position.y;

            returning = true;

        }

        if(returning)
        {
            float dis = Vector3.Distance(myAim, transform.position);
            if (dis < 0.5f)
            {
                myAim = new();
                returning = false;
            }
            else
                calForce = (myAim - transform.position) * weight;
        }

        return calForce;

    }

    private bool DistanceOut()
    {
        if (myBee == null || myBee.currentBox == null)
            return false;
        Vector3 otherLoc = myBee.currentBox.transform.position;

        Vector2 vec1 = new Vector2(transform.position.x, transform.position.z);
        Vector2 vec2 = new Vector2(otherLoc.x, otherLoc.z);

        bool check = Vector2.Distance(vec1, vec2) > maxRange;

        return check;

    }

}
