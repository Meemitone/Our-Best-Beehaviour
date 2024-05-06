using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HiveGeneticsManager : MonoBehaviour
{
 //extra stats cause i cant think :(
    private int BeeNumber;
    public float[] Benetics = new float[5];
    public GameObject[] Bees;
    [SerializeField] private List<BeeneticAlgorithm> BA = new List<BeeneticAlgorithm>();
    private int Beemoved;
    private GameObject Beefab;
    private void Awake()
    {
        BeeNumber = 0;
    }
    
    /*void Start()
    {
        
        for (int i = 0; i < 10; i++)
        {
            BeeNumber = i;

@@ -31,39 +31,26 @@ public class HiveGeneticsManager : MonoBehaviour
                Benetics[BeeNumber, j] = BA.Benes[j];
                //Debug.Log(Benetics[BeeNumber, i]);
            }
            
        }


    
    
    
    }*/

    private void OnTriggerEnter(Collider other) //will correct depending on how we decide to trigger a bee entering the hive. Adds a bees genes to the hive.
    {
        if (other.tag == "Bee")
        {
 
            BA.Add(other.GetComponent<BeeneticAlgorithm>());
            Benetics = other.GetComponent<BeeneticAlgorithm>().Benes;
            BeeNumber++;
            Destroy(other.GameObject());
        }
        
    }
    


    public void OnBeeLeaving()
    {
       Beemoved  = Random.Range(0,BeeNumber);
       Beefab.GetComponent<BeeneticAlgorithm>().Benes = BA.ElementAt(Beemoved).Benes;
       BA.RemoveAt(Beemoved);
       BeeNumber--;
    }
}
