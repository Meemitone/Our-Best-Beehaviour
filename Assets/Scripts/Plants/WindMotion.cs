using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMotion : Behaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Vector3 CalculateForce()
    {
        return EnvironmentalData.instance.GetWind(transform.position) * weight;
    }
}
