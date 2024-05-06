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

    [SerializeField] float hiveRange = 1.0f;

    private void Start()
    {
        myBee = GetComponent<BeeBoid>();
        active = false;
    }

    public void BeginArrive(Transform arriveHere)
    {
        arriveTarget = arriveHere;

        if (arriveHere.parent.TryGetComponent<BeeInteraction>(out BeeInteraction interact))
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

        calForce = Vector3.ClampMagnitude(calForce, weight);

        if(dis < 0.5f && arriveTarget.parent.tag == "Flower")
        {
            calForce = new();
            myBee.currentDirection = new();
            transform.parent = arriveTarget;

            active = false;
            myBee.polenHeld += arriveTarget.parent.GetComponent<Flower>().TakePollen(myBee.maxPolenHold - myBee.polenHeld, ref myBee.flowData);

            print("Ready to leave");
            StartCoroutine(PrepareLeave());

        }
        else if(dis < hiveRange && arriveTarget.tag == "Hive")
        {
            active = false;
            arriveTarget.GetComponent<HiveGeneticsManager>().OnBeeReturn(gameObject);
        }
        return calForce;

    }

    private IEnumerator PrepareLeave()
    {

        print("Ready to leave");

        yield return new WaitForSeconds(waitTime);


        if (arriveTarget.parent.TryGetComponent<BeeInteraction>(out BeeInteraction interact))
        {
            interact.takenByBee = false;
        }

        arriveTarget = null;

        transform.parent = null;
        if(myBee.polenHeld >= myBee.maxPolenHold)
        {

            BeginArrive(myBee.homeHive);
            GetComponent<Flock>().active = true;
        }
        else
            foreach (Behaviour beeB in myBee.behaviours)
                if (beeB != this)
                    beeB.active = true;


        print("Leaving now");
    }

}
