using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BoidBasic : MonoBehaviour
{

    public Vector3 force;
    public float maxForce = 6;
    public float gravity = 3;
    public Behaviour[] behaviours;


    internal virtual void Update()
    {

        force = new Vector3();
        foreach (Behaviour behaviour in behaviours)
        {
            force += behaviour.CalculateForce();
            if (force.magnitude > maxForce)
            {
                force = Vector3.ClampMagnitude(force, maxForce);
                break;
            }
        }
    }

}

[Serializable]
public abstract class Behaviour : MonoBehaviour
{

    public bool active = true;
    public float weight = 1.0f;//prioity

    public virtual Vector3 CalculateForce()
    {
        return new Vector3();
    }

}