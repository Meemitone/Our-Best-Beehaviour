using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BeeBoid : BoidBasic
{

    float acceleration = 2f;

    public Vector3 currentDirection;

    public float turnMax;

    public float polenHeld;

    public float maxPolenHold;

    public FlowerData flowData;

    public Transform homeHive;

    public List<BeeStat> beeStats = new List<BeeStat>();

    public float selfDistruct = 60;

    private void FixedUpdate()
    {

        Movement();
        Rotation();
        TryToDie();

    }

    public void Movement()
    {

        currentDirection += force * Time.deltaTime * acceleration * beeStats[0].statBase;

        float curClamp = Vector3.Magnitude(force);

        currentDirection = Vector3.ClampMagnitude(currentDirection, curClamp) * beeStats[1].statBase;

        transform.position += currentDirection * Time.deltaTime;

    }

    public void Rotation()
    {

        Vector3 currentUp = CalBank();

        Vector3 currentFor = CalForward(currentUp);

        if (currentDirection.magnitude > 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(currentFor, currentUp);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.5f);

        }

    }

    private Vector3 CalBank()
    {

        Vector3 calculateUp = gravity * Vector3.up;

        calculateUp += force;

        return calculateUp.normalized;

    }

    private Vector3 CalForward(Vector3 up)
    {

        Vector3 right = Vector3.Cross(currentDirection.normalized, up);

        Vector3 forward = Vector3.Cross(up, right);

        return forward;

    }

    private void Start()
    {

        BeeneticAlgorithm statGen = GetComponent<BeeneticAlgorithm>();

        for (int i = 0; i < beeStats.Count; i++)
        {
            beeStats[i].statBase += beeStats[i].maxShift * statGen.Benes[i];

            if (beeStats[i].isInt)
                beeStats[i].statBase = Mathf.RoundToInt(beeStats[i].statBase);
        }

        maxPolenHold = beeStats[4].statBase;

        selfDistruct = beeStats[5].statBase;

        Box[] boxes = FindObjectsOfType<Box>();

    }

    private void TryToDie()
    {
        selfDistruct -= Time.deltaTime;

        if (selfDistruct <= 0)
            Destroy(gameObject);
    }

}

[Serializable]
public class BeeStat
{

    public string name;
    public float statBase;
    public float maxShift;
    public bool isInt;

}