using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class BoidBasic : MonoBehaviour
{

    public Vector3 force;
    public float maxForce = 6;
    public float gravity = 3;
    public Behaviour[] behaviours;

    public Box currentBox;

    internal virtual void Update()
    {

        force = new Vector3();
        foreach (Behaviour behaviour in behaviours)
        {
            if(behaviour.active)
                force += behaviour.CalculateForce();
            if (force.magnitude > maxForce)
            {
                force = Vector3.ClampMagnitude(force, maxForce);
                break;
            }
        }
    }

    internal virtual void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Box")
        {
            if(currentBox != null)
                currentBox.objectsInBox.Remove(transform);

            currentBox = other.GetComponent<Box>();

            currentBox.objectsInBox.Add(transform);
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