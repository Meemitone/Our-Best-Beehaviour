using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeneticAlgorithm : MonoBehaviour
{
    public float[] Benes = new float[5];
    public bool NewBee;
    
    
    // Start is called before the first frame update
    void Start()
    {
        LateStart();
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.01f);
        if (!NewBee)
        {
            for (int i = 0; i < Benes.Length; i++)
            {
                Benes[i] = Random.Range(0.0f, 1.0f);
            }
        }
            
    }

}
