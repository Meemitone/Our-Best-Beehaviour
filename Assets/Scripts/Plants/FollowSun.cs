using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowSun : Behaviour
{
    private void Start()
    {
        
    }

    public override Vector3 CalculateForce()
    {
        Vector3 LD = -EnvironmentalData.instance.transform.forward;
        return LD * weight;
    }
}
