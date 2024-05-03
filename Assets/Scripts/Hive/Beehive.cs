using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Beehive : MonoBehaviour
{
    [Header("Pollen")]
    public float pollen = 100;
    [SerializeField] private float pollenMaximum = 100;
    [SerializeField] private float pollenDeathBand = 10;
    [SerializeField] private float pollenHealthyBand = 80;
    [SerializeField] private float pollenDecayPerSecond = 0.1f;

    [Header("Bees")]
    public int beeCount;
    [SerializeField] private float BeePollenCost = 10f;
    [SerializeField] private GameObject beePrefab;
    [SerializeField] private GameObject beeSpawnLocation;
    [SerializeField] private GameObject beeParent;

    [Header("Self")] 
    [SerializeField] private float health;
    [SerializeField] private float healthMaximum = 3;
    [SerializeField] private float stateCheckTime = 5;
    [SerializeField] private float stateCheckTimer;


    private void Awake()
    {
        stateCheckTimer = stateCheckTime;
        health = healthMaximum;
        ReleaseBee(3);
    }
    
    
    private void FixedUpdate()
    {
        if (stateCheckTimer <= 0)
        {
            StateCheck();
            stateCheckTimer = stateCheckTime;
        }
        else
        {
            stateCheckTimer -= Time.fixedDeltaTime;
        }

        pollen -= pollenDecayPerSecond * Time.fixedDeltaTime;
    }
    
    
    

    private void StateCheck()
    {
        if (pollen <= pollenDeathBand) health --;
        if(health <= 0) Destroy(gameObject);
        if (pollen >= pollenHealthyBand)
        {
            ReleaseBee(2);
            health++;
        }
    }

    public void DonatePollen(float amount)
    {
        pollen += amount;
        if (pollen > pollenMaximum) pollen = pollenMaximum;
    }

    public void ReleaseBee(int number)
    {
        for (int i = 0; i < number; i++)
        {
            beeCount++;
            pollen -= BeePollenCost;
            Instantiate
            (
                beePrefab, 
                beeSpawnLocation.transform.position + new Vector3(Random.Range( -0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * transform.localScale.x,
                transform.rotation,
                beeParent.transform
            );
        }
    }

}
