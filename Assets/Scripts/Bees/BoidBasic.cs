using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BoidBasic : MonoBehaviour
{

    [SerializeField] Vector3 force;
    [SerializeField] float maxForce = 6;
    [SerializeField] Behaviour[] behaviours;


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

    public bool active;
    public float weight = 1.0f;//prioity

    public virtual Vector3 CalculateForce()
    {
        return new Vector3();
    }

}