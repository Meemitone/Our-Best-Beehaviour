using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowSun : Behaviour
{

    public override Vector3 CalculateForce()
    {
        Vector3 LD = -EnvironmentalData.Instance.transform.forward;
        return LD * weight;
    }
}
