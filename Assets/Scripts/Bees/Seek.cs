
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Behaviour
{

    Transform tarFlower;

    bool checkFlower = true;

    [SerializeField] float timeBetweenChecks = 1f;

    Vector3 calForce = new();
    public override Vector3 CalculateForce()
    {
        if (checkFlower)
        {
            calForce = new();
            Box curBox = GetComponent<BeeBoid>().currentBox;


            if (tarFlower == null || tarFlower.GetComponent<BeeInteraction>().takenByBee)
            {
                Transform tarFlower = null;
                float flowDis = 0;
                foreach (Transform flower in curBox.objectsInBox)
                {
                    if (flower.tag == "Flower")
                    {
                        if (tarFlower == null && !flower.GetComponent<BeeInteraction>().takenByBee)
                        {
                            tarFlower = flower;
                            flowDis = Vector3.Distance(transform.position, flower.position);
                        }
                        else if (flowDis > Vector3.Distance(transform.position, flower.position) && !flower.GetComponent<BeeInteraction>().takenByBee)
                        {
                            tarFlower = flower;
                            flowDis = Vector3.Distance(transform.position, flower.position);
                        }

                    }
                }
                if (tarFlower == null)
                {
                    foreach (Box box in curBox.neighbours)
                    {
                        foreach (Transform flower in box.objectsInBox)
                        {
                            if (flower.tag == "Flower")
                            {
                                if (tarFlower == null && !flower.GetComponent<BeeInteraction>().takenByBee)
                                {
                                    tarFlower = flower;
                                    flowDis = Vector3.Distance(transform.position, flower.position);
                                }
                                else if (flowDis > Vector3.Distance(transform.position, flower.position) && !flower.GetComponent<BeeInteraction>().takenByBee)
                                {
                                    tarFlower = flower;
                                    flowDis = Vector3.Distance(transform.position, flower.position);
                                }

                            }
                        }
                        if (tarFlower != null)
                            break;
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
            && GetComponent<Arrive>().stopRange >= Vector3.Distance(tarFlower.position, transform.position))
            GetComponent<Arrive>().BeginArrive(tarFlower);

        return calForce;
    }

    private IEnumerator CheckAgain()
    {

        yield return new WaitForSeconds(timeBetweenChecks);

        checkFlower = true;

    }

}