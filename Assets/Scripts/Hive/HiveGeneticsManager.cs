using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HiveGeneticsManager : MonoBehaviour
{
    private int Beenome;
    private int ParentA;
    private int ParentB;
    private int BeeNumber;
    private int BeesToMake;
    public float[] Baybee = new float[5];
    //[SerializeField] private List<BeeneticAlgorithm> BA = new List<BeeneticAlgorithm>();
    private int Beemoved;
    private GameObject Beefab;
    [SerializeField] private Vector3 Spawnpoint;
    private List<float[]> BA = new List<float[]>();     //might need this to replace BA
    
    private void OnTriggerEnter(Collider other) //will correct depending on how we decide to trigger a bee entering the hive. Adds a bees genes to the hive when they enter.
    {
        if (other.tag == "Bee")
        {

            BA[BeeNumber] = other.GetComponent<BeeneticAlgorithm>().Benes;
            BeeNumber++;
            Destroy(other.GameObject());
        }
        
    }
    
    public void OnBeeLeaving(int Beeleavers) // call OnBeeLeaving(int) to spawn a bees in the hive equal to the chosen number or until hive is empty
    {
        for (int i = 0; i < Beeleavers; i++)
        {
            if (BeeNumber > 0)
            {
                Beemoved = Random.Range(0, BeeNumber);
                Beefab.GetComponent<BeeneticAlgorithm>().Benes = BA[Beemoved];  
                Beefab.GetComponent<BeeneticAlgorithm>().NewBee = true;
                Instantiate(Beefab, Spawnpoint, Quaternion.identity);
                BA.RemoveAt(Beemoved);
                BeeNumber--;
            }
            else
            {
                break;
            }
        }
    }


    public void Beeproduction(int NewBees)
    {
        BeesToMake = NewBees;
        for (int x = 0; x < NewBees; x++)
        {
            ParentA = Random.Range(0, BeeNumber);
            ParentB = Random.Range(0, BeeNumber);
            if (ParentA == ParentB)
            {
                Beeproduction(BeesToMake);
            }
            else if( ParentA != ParentB)
            {
                for (int i = 0; i < 5; i++)
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

                    if (i == 5)
                    {
                        BA[BeeNumber] = Baybee;
                        BeeNumber++;
                        BeesToMake--;
                    }
                }
            }
        }
    }
}
