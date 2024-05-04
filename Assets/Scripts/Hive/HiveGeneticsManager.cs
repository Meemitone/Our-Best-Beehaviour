using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HiveGeneticsManager : MonoBehaviour
{
 //extra stats cause i cant think :(
    private int BeeNumber;
    private BeeneticAlgorithm BA;
    public int[,] Benetics = new int[10,5];
    private int MaxBees;
    public GameObject[] Bees;

    private void Awake()
    {
        MaxBees = 10;
        BeeNumber = 0;
    }

    void Start()
    {
        
        for (int i = 0; i < 10; i++)
        {
            BeeNumber = i;
            for (int j = 0; j < 5; j++)
            {
                BA = Bees[j].GetComponent<BeeneticAlgorithm>();
                Benetics[BeeNumber, j] = BA.Benes[j];
                //Debug.Log(Benetics[BeeNumber, i]);
            }
            
        }

        for(int i = 0; i < 10; i++)
        {
            Debug.Log("This bees Weight is " + Benetics[i,0]);
            Debug.Log("This bees Speed is " + Benetics[i,1]);
            Debug.Log("This bees Fear is " + Benetics[i,2]);
            Debug.Log("This bees Solidarity is " + Benetics[i,3]);
            Debug.Log("This bees Strength is " + Benetics[i,4]);
        }
    }
    
    
    
    
    
    
    

    private void OnTriggerEnter(Collider other) //will correct depending on how we decide to trigger a bee entering the hive
    {
        if (other.tag == "Bee")
        {
            BA = other.GetComponent<BeeneticAlgorithm>();
            for (int i = 0; i > MaxBees; i++)
            {
                Benetics[BeeNumber,i] = BA.Benes[i];
                print(Benetics[BeeNumber,i]);
            }
            BeeNumber++;
        }
        
    }
    
}
