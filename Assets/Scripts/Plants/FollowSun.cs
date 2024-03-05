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
        Quaternion lightDirection = EnvironmentalSunlight.instance.transform.rotation;
        Vector3 LD = lightDirection * Vector3.forward;
        LD = -LD; //backwards
        return LD * weight;
    }
}
