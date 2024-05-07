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
    public Vector3 Spawnpoint;
    public List<float[]> BA;
    public float respawnTime;
    public int spawnCount;

   
    void Awake()
    {
        BA = new List<float[]>();
        for(int i = 0; i<3; i++)
        {
            float[] tempCopy = new float[6];
            for (int j = 0; j < 6; j++)
            {
                tempCopy[j] = Random.Range(0f, 1f);
            }
            BA.Add(tempCopy);
        }
        StartCoroutine(HiveTimer());
    }
    public void OnBeeReturn(GameObject other) //will correct depending on how we decide to trigger a bee entering the hive. Adds a bees genes to the hive when they enter.
    {
        

        if (other.gameObject.CompareTag("Bee"))
        {
            float[] tempCopy = new float[6];
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
                GameObject Baybee = Instantiate(Beefab, Spawnpoint, Quaternion.identity);
                Baybee.GetComponent<BeeneticAlgorithm>().Benes = BA[Beemoved];
                Baybee.GetComponent<BeeneticAlgorithm>().NewBee = true;
                Baybee.GetComponent<BeeBoid>().homeHive = transform;
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
        List<float[]> baybeBees = new();
        if (BA.Count > 1)
        {
            for (int x = 0; x < NewBees; x++)
            {
                float[] Baybee = new float[6];
                ParentA = Random.Range(0, BA.Count);
                ParentB = Random.Range(0, BA.Count - 1);
                if(ParentB >= ParentA)
                {
                    ParentB++;
                    ParentB %= BA.Count;
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
                        baybeBees.Add(Baybee); 
                    }
                }
            }
        }
        else
        {
            for (int x = 0; x < NewBees; x++)
            {
                float[] Baybee = new float[6];
                for (int i = 0; i < 5; i++) // picks genes at random from both parents and mutates them slightly;
                {
                    Baybee[i] = Random.Range(0f,1f);

                    if (i == 4)
                    {
                        baybeBees.Add(Baybee);
                    }
                }
            }
        }
        BA.Clear();
        BA.AddRange(baybeBees);
    }

    public System.Collections.IEnumerator HiveTimer()
    {
        yield return new WaitForSeconds(Random.Range(0f, 5f));
        yield return new WaitForSeconds(respawnTime);
        Beeproduction(spawnCount);
        OnBeeLeaving(BA.Count);
        StartCoroutine(HiveTimer());
    }

}
