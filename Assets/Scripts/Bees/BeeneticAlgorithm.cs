using System.Collections;
using UnityEngine;

public class BeeneticAlgorithm : MonoBehaviour
{
    public float[] Benes = new float[5];
    public bool NewBee;
    
    void Start()
    {
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        if (!NewBee)
        {
            for (int i = 0; i < Benes.Length; i++)
            {
                Benes[i] = Random.Range(0.0f, 1.0f);
            }
        }
    }
}
