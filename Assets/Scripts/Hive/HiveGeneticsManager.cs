using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HiveGeneticsManager : MonoBehaviour
{
    private int Beenome;
    private int ParentA;
    private int ParentB;
    private int Beemoved;
    [SerializeField] private GameObject Beefab;
    [SerializeField] private Vector3 Spawnpoint;
    private List<float[]> BA = new List<float[]>();

    void Start()
    {
        for(int i = 0; i<3; i++)
        {
            float[] tempCopy = new float[5];
            for (int j = 0; j < 5; j++)
            {
                tempCopy[j] = Random.Range(0f, 1f);
            }
            BA.Add(tempCopy);
        }
    }
    private void OnTriggerEnter(
            Collider other) //will correct depending on how we decide to trigger a bee entering the hive. Adds a bees genes to the hive when they enter.
    {
        

        if (other.gameObject.CompareTag("Bee"))
        {
            float[] tempCopy = new float[5];
            other.GetComponent<BeeneticAlgorithm>().Benes.CopyTo(tempCopy, 0);
            BA.Add(tempCopy);
            Destroy(other.GameObject());
        }
    }

    public void OnBeeLeaving(int Beeleavers) // call OnBeeLeaving(int) to spawn a bees in the hive equal to the chosen number or until hive is empty
    {
        for (int i = 0; i < Beeleavers; i++)
        {
            if (BA.Count > 0)
            {
                Beemoved = Random.Range(0, BA.Count);
                Beefab.GetComponent<BeeneticAlgorithm>().Benes = BA[Beemoved];
                Beefab.GetComponent<BeeneticAlgorithm>().NewBee = true;
                Instantiate(Beefab, Spawnpoint, Quaternion.identity);
                BA.RemoveAt(Beemoved);
            }
            else
            {
                break;
            }
        }
    }


    public void Beeproduction(int NewBees)
    {
        float[] Baybee = new float[5];
        if (BA.Count > 1)
        {
            for (int x = 0; x < NewBees; x++)
            {
                ParentA = Random.Range(0, BA.Count);
                ParentB = Random.Range(0, BA.Count);
                while (ParentA == ParentB && BA.Count > 1)
                {
                    ParentB = Random.Range(0,BA.Count); // in case this call uses the same bee on itself for reproduction, makes the call again with remaining number of desired bees.
                }
                for (int i = 0; i < 5; i++) // picks genes at random from both parents and mutates them slightly;
                {
                    Beenome = Random.Range(0, 2);
                    if (Beenome == 1)
                    {
                        Baybee[i] = BA[ParentA][i] + Random.Range(-0.01f, 0.01f);
                    }
                    else if (Beenome != 1)
                    { 
                        Baybee[i] = BA[ParentB][i] + Random.Range(-0.01f, 0.01f);
                    }

                    if (Baybee[i] > 1f)
                    { 
                        Baybee[i] = 1f;
                    }

                    if (Baybee[i] < 0f)
                    { 
                        Baybee[i] = 0f;
                    }

                    if (i == 4)
                    { 
                        BA.Add(Baybee); 
                    }
                }
            }
        }
    }
}
