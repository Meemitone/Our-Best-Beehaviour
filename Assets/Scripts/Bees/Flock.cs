using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flock : Behaviour
{

    [SerializeField] float minDistance = 1f;

    [SerializeField] float maxDistance = 5f;

    [SerializeField] float pushAway = 2f;

    public struct BeeBoi
    {
        public BeeBoid beeBody;
        public float beeDis;
        public Vector3 beeDir;
    }

    public List<BeeBoi> myBois = new();

    private bool beecheck = true;

    [SerializeField] int numOfB = 3;
    [SerializeField] float timeForCheck = 1f;

    private BeeBoid beeBody;

    Vector3 calForce = new();

    private void Start()
    {
        beeBody = GetComponent<BeeBoid>();

        weight = beeBody.beeStats[3].statBase;
    }

    public override Vector3 CalculateForce()
    {

        if (beecheck && beeBody.currentBox != null)
        {

            foreach (Transform bee in beeBody.currentBox.objectsInBox)
            {

                if (bee != null && bee.tag == "Bee")
                {
                    BeeBoi curBee = new();

                    curBee.beeDis = Vector3.Distance(transform.position, bee.position);
                    if (bee.GetComponent<Flock>().active && !CheckBee(bee) && curBee.beeDis < maxDistance)
                    {

                        curBee.beeDir = bee.position - transform.position;
                        curBee.beeBody = bee.GetComponent<BeeBoid>();
                        myBois.Add(curBee);
                        if (myBois.Count >= numOfB)
                            break;
                    }
                }

            }

            if (myBois.Count < numOfB)
            {
                foreach (Box box in beeBody.currentBox.neighbours)
                {
                    foreach (Transform bee in box.objectsInBox)
                    {

                        if (bee != null && bee.tag == "Bee")
                        {
                            BeeBoi curBee = new();

                            curBee.beeDis = Vector3.Distance(transform.position, bee.position);
                            if (curBee.beeDis < maxDistance)
                            {

                                curBee.beeDir = bee.position - transform.position;
                                curBee.beeBody = bee.GetComponent<BeeBoid>();
                                myBois.Add(curBee);

                                if (myBois.Count >= numOfB)
                                    break;

                            }
                        }

                    }

                    if (myBois.Count >= numOfB)
                        break;
                }
            }

            foreach (BeeBoi beeBoi in myBois)
            {
                if (beeBoi.beeBody == null)
                    continue;
                calForce += beeBoi.beeBody.force;
                if (beeBoi.beeDis < minDistance)
                    calForce += beeBoi.beeDir * -pushAway;

                beeBoi.beeBody.GetComponent<Flock>().beeBody = beeBody;
            }

            calForce = Vector3.ClampMagnitude(calForce, weight);

            beecheck = false;

            StartCoroutine(Recheck());
        }

        myBois.Clear();

        return calForce;

    }

    private IEnumerator Recheck()
    {
        yield return new WaitForSeconds(timeForCheck);
        beecheck = true;
    }

    private bool CheckBee(Transform beeCheck)
    {
        bool same = false;

        foreach(BeeBoi beeBoi in myBois)
        {
            if(beeBoi.beeBody == beecheck)
            {
                same = true;
                break;
            }
        }

        return same;
    }

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


