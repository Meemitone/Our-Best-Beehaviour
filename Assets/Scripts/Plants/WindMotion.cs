using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMotion : Behaviour
{
    public override Vector3 CalculateForce()
    {
        return EnvironmentalData.Instance.GetWind(transform.position) * weight;
    }
}
