using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class BitInteractions : MonoBehaviour
{
    public float f;
    [SerializeField] byte[] bytes;
    public bool btf = false;



    [SerializeField] bool mutate = false;


    [SerializeField] float changeChance = 0.1f;
    [SerializeField] float result = 0.5f;
    float Result
    {
        get { return result; }
        set
        {
            if (UnityEngine.Random.value > changeChance)
                result = value;
            else
            {
                int bits = BitConverter.SingleToInt32Bits(value);
                int change = UnityEngine.Random.Range(0,10);
                if (change > 6)
                    change++;
                    change += 16;
                int modifier = 1 << change;
                bits ^= modifier;
                float answer = BitConverter.Int32BitsToSingle(bits);
                if(answer > 1)
                {
                    Debug.Log(modifier);
                }
                result = answer;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bytes = new byte[4];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (btf)
            f = BitConverter.ToSingle(bytes, 0);
        else
            bytes = BitConverter.GetBytes(result);
        

        if(mutate)
        {
            Result = Result;
            mutate = false;
        }
    }
}
