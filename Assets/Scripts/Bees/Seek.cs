
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Behaviour
{

    Transform tarFlower;

    bool checkFlower = true;

    [SerializeField] float timeBetweenChecks = 1f;

    Vector3 calForce = new();

    private Transform lastFlower;

    private void Start()
    {
        lastFlower = transform;
    }

    public override Vector3 CalculateForce()
    {
        if (checkFlower)
        {
            calForce = new();
            Box curBox = GetComponent<BeeBoid>().currentBox;
            if(curBox == null)
            {
                return Vector3.zero;
            }

            if (tarFlower == null || tarFlower.GetComponent<BeeInteraction>().takenByBee)
            {
                tarFlower = null;

                float flowDis = 0;
                {
                    foreach (Box box in curBox.neighbours)
                    {
                        foreach (Transform flower in box.objectsInBox)
                        {
                            if (flower!=null && flower.tag == "Flower")
                            {
                                if (tarFlower == null && !flower.GetComponent<BeeInteraction>().takenByBee && flower.GetComponent<Flower>().pollen >= 1 && flower != lastFlower)
                                {
                                    tarFlower = flower;
                                    flowDis = Vector3.Distance(transform.position, flower.position);
                                }
                                else if (flowDis > Vector3.Distance(transform.position, flower.position) && !flower.GetComponent<BeeInteraction>().takenByBee
                                    && flower.GetComponent<Flower>().pollen >= 1 && flower != lastFlower)
                                {
                                    tarFlower = flower;
                                    flowDis = Vector3.Distance(transform.position, flower.position);
                                }

                            }
                        }
                    }
                }
            }
            if (tarFlower != null)
            {
                GetComponent<Wander>().active = false;
                calForce = (tarFlower.GetComponent<BeeInteraction>().targetPoint.position - transform.position) * weight;


            }
            else
            {
                GetComponent<Wander>().active = true;
            }

            checkFlower = false;
            StartCoroutine(CheckAgain());

        }

        if (tarFlower != null && !tarFlower.GetComponent<BeeInteraction>().takenByBee
            && GetComponent<Arrive>().stopRange >= Vector3.Distance(tarFlower.GetComponent<BeeInteraction>().targetPoint.position, transform.position))
        {
            print("Arrived");
            GetComponent<Arrive>().BeginArrive(tarFlower.GetComponent<BeeInteraction>().targetPoint);
            lastFlower = tarFlower;
            tarFlower = null;
        }
        return calForce;
    }

    private IEnumerator CheckAgain()
    {

        yield return new WaitForSeconds(timeBetweenChecks);

        checkFlower = true;

    }

}