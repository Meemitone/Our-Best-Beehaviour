using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Arrive : Behaviour
{

    private BeeBoid myBee;

    public float stopRange = 3f;

    private Transform arriveTarget;

    public float waitTime = 2f;

    private void Start()
    {
        myBee = GetComponent<BeeBoid>();
        active = false;
    }

    public void BeginArrive(Transform arriveHere)
    {
        arriveTarget = arriveHere;

        if (arriveHere.TryGetComponent<BeeInteraction>(out BeeInteraction interact))
            interact.takenByBee = true;

        active = true;

        foreach(Behaviour beeB in myBee.behaviours)
        {
            if (beeB != this)
                beeB.active = false;
        }

    }

    public override Vector3 CalculateForce()
    {

        Vector3 calForce = new();

        calForce = arriveTarget.position - transform.position;

        float dis = Vector3.Distance(transform.position, arriveTarget.position);

        calForce *= dis;

        if(dis < 0.1f && arriveTarget.tag == "Flower")
        {
            calForce = new();
            myBee.currentDirection = new();

            arriveTarget.GetComponent<Flower>().TakePollen(myBee.maxPolenHold - myBee.polenHeld, ref myBee.flowData);

            StartCoroutine(PrepareLeave());

        }
        else if(dis < 0.1f && arriveTarget.tag == "Hive")
        {


        }
        return calForce;

    }

    private IEnumerator PrepareLeave()
    {

        yield return new WaitForSeconds(waitTime);

        if (arriveTarget.TryGetComponent<BeeInteraction>(out BeeInteraction interact))
            interact.takenByBee = true;

        arriveTarget = null;

        active = false;

        foreach (Behaviour beeB in myBee.behaviours)
        {
            if (beeB != this)
                beeB.active = true;
        }

    }

}
