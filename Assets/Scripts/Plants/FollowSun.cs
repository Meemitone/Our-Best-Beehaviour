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
        Vector3 LD = -EnvironmentalSunlight.instance.transform.forward;
        return LD * weight;
    }
}
