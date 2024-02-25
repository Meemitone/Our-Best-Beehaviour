using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class HornetNavigation : MonoBehaviour
{

    [Header("Moving")]
    public Vector3 currentDirection = Vector3.forward;
    [SerializeField] private Vector3 targetDirection;
    [SerializeField] private float speed = 0;
    [SerializeField] private float maxSpeed = 2;
    [SerializeField] private float acceleration = 1;
    
    [Header("Wandering")]
    [SerializeField] private Vector3 wanderTargetDirection = Vector3.forward;
    [SerializeField] private Vector3 wanderDirection = Vector3.forward;

    [Header("Flocking")]
    [SerializeField] private float neighbourRadius = 5;
    [SerializeField] private List<HornetNavigation> neighbours;
    [SerializeField] private Vector3 neighboursCenter;
    [SerializeField] private Vector3 neighboursDirection;
    [SerializeField] private Vector3 followNeighboursCenter;

    [SerializeField] private float tooCloseRadius = 2;
    [SerializeField] private List<HornetNavigation> tooClose;
    [SerializeField] private Vector3 tooCloseAvoidance;
    [SerializeField] private float tooCloseAvoidanceStrength;
    
    [SerializeField] private List<HornetNavigation> hornets;

    [Header("Feeding")] 
    [SerializeField] private Vector3 closestFood;

    [Header("HomeBound")] 
    [SerializeField] private Vector3 homePosition;



    private void FixedUpdate()
    {
        CheckDirections();
        ApplyWeights();
    
        Movement();
        
        DisplayVectors();
    }



    
    private void CheckDirections()
    {
        CheckWanderDirection();
        CheckFlockDirection();
        CheckFoodDirection();
        CheckHomeDirection();
    }

    private void ApplyWeights()
    {
        targetDirection = Vector3.zero;
        targetDirection += wanderDirection.normalized;
        targetDirection += followNeighboursCenter.normalized;
        targetDirection += neighboursDirection.normalized;
        targetDirection += tooCloseAvoidance.normalized;
    }



    void Movement()
    {
        currentDirection = Vector3.RotateTowards(currentDirection, targetDirection, 0.02f, 0).normalized;
        transform.Translate(currentDirection * speed);
    }

    void DisplayVectors() // shows all of the vectors
    {
        Debug.DrawLine(transform.position, transform.position + currentDirection * 2, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + targetDirection.normalized * 2, Color.white);
        
        Debug.DrawLine(transform.position, transform.position + wanderDirection.normalized, Color.cyan);
        //Debug.DrawLine(transform.position, transform.position + wanderTargetDirection, Color.blue);


        Debug.DrawLine(transform.position, neighboursCenter, Color.gray);
        Debug.DrawLine(neighboursCenter, neighboursCenter + neighboursDirection * neighbours.Count, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + tooCloseAvoidance, Color.gray);
        

    }
    
    
    
    
    void CheckWanderDirection() // makes wanderDirection slowly rotate towards wanderTargetDirection, then randomizes the target on arrival
        // to make a smooth transition between different wandering choices
    { 
        if (Vector3.Angle(wanderDirection,wanderTargetDirection) < 0.5f)
        {
            wanderTargetDirection = new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
        }
        else
        {
            wanderDirection = Vector3.RotateTowards(wanderDirection, wanderTargetDirection, 0.01f, 0).normalized;
        }
    }

    
    
    
    void CheckFlockDirection()
    {
        // creates the lists
        neighbours = new List<HornetNavigation>();
        tooClose = new List<HornetNavigation>();
        hornets = FindObjectsOfType<HornetNavigation>().ToList();
        
        for (int i = 0; i < hornets.Count; i++)
        {

            float dist = (hornets[i].transform.position - transform.position).magnitude;

            if (dist < neighbourRadius)
            {
                neighbours.Add(hornets[i]);
            }

            if (dist < tooCloseRadius &&  hornets[i] != this)
            { 
                tooClose.Add(hornets[i]);
            }
            
        }

        // finds flock center and heading
        Vector3 flockCenter = Vector3.zero;
        Vector3 flockDirection = Vector3.zero;

        for (int i = 0; i < neighbours.Count; i++)
        {
            flockCenter += neighbours[i].transform.position;
            flockDirection += neighbours[i].currentDirection;
        }

        neighboursCenter = (flockCenter / neighbours.Count);
        followNeighboursCenter = neighboursCenter - transform.position;
        neighboursDirection = Vector3.zero + (flockDirection / neighbours.Count);

        Vector3 avoidance = Vector3.zero;
        
        // avoids nearby agents
        for (int i = 0; i < tooClose.Count; i++)
        {
            avoidance += -(tooClose[i].transform.position - transform.position).normalized;
        }

        tooCloseAvoidance = Vector3.zero + (avoidance / tooClose.Count);

    }

    
    
    
    void CheckFoodDirection()
    {
        
    }

    
    
    
    void CheckHomeDirection()
    {
        
    }
}
