using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    [SerializeField] GameObject Segment;
    [SerializeField] GameObject Leaf;
    [SerializeField] GameObject Flower;


    [SerializeField] FlowerData genetics;

    public bool debugMutate = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debugMutate)
        {
            debugMutate = false;
            genetics = FlowerData.Copy(genetics);
        }
    }


}


[Serializable]
public class FlowerData
{
    [Header("L,S,F only (for Leaf Segment and Flower (for now))")]
    public string geneCode;//Segment Segment Segment Leaf Leaf Leaf Segment Flower

    public float growRate;
    public float segmentSize;
    public float segmentSizeRatio; //if 0.9, then each segment is 0.9x the size of the previous

    public float leafSize;

    public int seedCount;


    public float energy;

    public string GeneCode
    {
        get
        {
            return geneCode;
        }
        set
        {//introduce rng here
            geneCode = value;
        }
    }

    public FlowerData()
    {//default values
        geneCode = "SSSLLLSF";
        growRate = 0.1f;
        segmentSize = 1f;
        segmentSizeRatio = 0.95f;
        leafSize = 1f;
        seedCount = 3;
        energy = 7f;
    }

    public FlowerData(string Code, float gR, float sS, float sSR, float lS, int sC, float e)
    {//custom Values
        geneCode = Code;
        growRate = gR;
        segmentSize = sS;
        segmentSizeRatio = sSR;
        leafSize = lS;
        seedCount = sC;
        energy = e;
    }


    public static FlowerData Copy(FlowerData source, float Energy = 7, bool mutate = true)
    {//when making a copy {FlowerData seed = new Flowerdata(parentPlantData)}
        FlowerData result = new FlowerData();

        result.energy = Energy;
        if(!mutate)
        {
            result.geneCode = source.geneCode;
            result.growRate = source.growRate;
            result.segmentSize = source.segmentSize;
            result.segmentSizeRatio = source.segmentSizeRatio;
            result.leafSize = source.leafSize;
            result.seedCount = source.seedCount;
        }
        else
        {
            if(UnityEngine.Random.value > MutateGlobal.instance.plantMutateChance)
            {
                result.growRate = source.growRate;
            }
            else
            {
                int bits = BitConverter.SingleToInt32Bits(source.growRate);
                int change = UnityEngine.Random.Range(0, 10);
                if (change > 6)
                    change++;
                change += 16;
                int modifier = 1 << change;
                bits ^= modifier;
                float answer = BitConverter.Int32BitsToSingle(bits);
                result.growRate = answer;
            }

            if (UnityEngine.Random.value > MutateGlobal.instance.plantMutateChance)
            {
                result.segmentSize = source.segmentSize;
            }
            else
            {
                int bits = BitConverter.SingleToInt32Bits(source.segmentSize);
                int change = UnityEngine.Random.Range(0, 10);
                if (change > 6)
                    change++;
                change += 16;
                int modifier = 1 << change;
                bits ^= modifier;
                float answer = BitConverter.Int32BitsToSingle(bits);
                result.segmentSize = answer;
            }

            if (UnityEngine.Random.value > MutateGlobal.instance.plantMutateChance)
            {
                result.segmentSizeRatio = source.segmentSizeRatio; //probably shouldn't change actually
            }
            else
            {
                result.segmentSizeRatio = source.segmentSizeRatio; //probably shouldn't change actually
                /*
                int bits = BitConverter.SingleToInt32Bits(source.segmentSizeRatio);
                int change = UnityEngine.Random.Range(0, 10);
                if (change > 6)
                    change++;
                change += 16;
                int modifier = 1 << change;
                bits ^= modifier;
                float answer = BitConverter.Int32BitsToSingle(bits);
                if (answer > 1)
                {
                    Debug.Log(modifier);
                }
                segmentSizeRatio = answer;
                */
            }

            if (UnityEngine.Random.value > MutateGlobal.instance.plantMutateChance)
            {
                result.leafSize = source.leafSize;
            }
            else
            {
                int bits = BitConverter.SingleToInt32Bits(source.leafSize);
                int change = UnityEngine.Random.Range(0, 10);
                if (change > 6)
                    change++;
                change += 16;
                int modifier = 1 << change;
                bits ^= modifier;
                float answer = BitConverter.Int32BitsToSingle(bits);
                result.leafSize = answer;
            }
        }

        result.geneCode = "";
        for(int i = 0; i < source.geneCode.Length; i++)
        {
            if(UnityEngine.Random.value > MutateGlobal.instance.plantMutateChance)
            {
                result.geneCode += source.geneCode[i];
            }
            else
            {
                float rngVal = UnityEngine.Random.value;
                if (rngVal < MutateGlobal.instance.plantAddChance)
                {
                    result.geneCode += MutateGlobal.instance.GetRandomGeneCodeLetter();//add a random letter
                    result.geneCode += source.geneCode[i];//before putting the next one in
                }
                else if (rngVal <  MutateGlobal.instance.plantDeleteChance + MutateGlobal.instance.plantAddChance)
                {
                    //do nothing, ie, skip the next letter
                }
                else if (rngVal < MutateGlobal.instance.plantDeleteChance + MutateGlobal.instance.plantAddChance + MutateGlobal.instance.plantReplaceChance)
                {
                    result.geneCode += MutateGlobal.instance.GetRandomGeneCodeLetter();//add a random letter
                    //and skip next letter, therby replacing
                }
            }
        }
        if(result.geneCode == "")
        {
            result.geneCode += 'F';
        }


        return result;
        /*
        {
            if (UnityEngine.Random.value > changeChance)
                result = value;
            else
            {
                int bits = BitConverter.SingleToInt32Bits(value);
                int change = UnityEngine.Random.Range(0, 10);
                if (change > 6)
                    change++;
                change += 16;
                int modifier = 1 << change;
                bits ^= modifier;
                float answer = BitConverter.Int32BitsToSingle(bits);
                if (answer > 1)
                {
                    Debug.Log(modifier);
                }
                result = answer;
            }
        }
        */
    }
}
