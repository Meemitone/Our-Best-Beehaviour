using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeneticAlgorithm : MonoBehaviour
{
    public int[] Benes = new int[5];
    
    
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Benes.Length; i++)
        {
            Benes[i] = Random.Range(1, 10);
        }
    }


}
