using System;
using UnityEngine;

[Serializable]
public class FlowerData
{
    [Header("L,S,F only (for Leaf Segment and Flower (for now))")]
    public string geneCode;//Segment Segment Segment Leaf Leaf Leaf Segment Flower //SSSLLLSF

    public float growRate;
    public float segmentSize;
    public float segmentSizeRatio; //if 0.9, then each segment is 0.9x the size of the previous

    public float leafSize;

    public int seedCount;


    public float energy;

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
        FlowerData result = new();

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
            if(UnityEngine.Random.value > MutateGlobal.Instance.plantMutateChance)
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

            if (UnityEngine.Random.value > MutateGlobal.Instance.plantMutateChance)
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

            if (UnityEngine.Random.value > MutateGlobal.Instance.plantMutateChance)
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

            if (UnityEngine.Random.value > MutateGlobal.Instance.plantMutateChance)
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
            if(UnityEngine.Random.value > MutateGlobal.Instance.plantMutateChance)
            {
                result.geneCode += source.geneCode[i];
            }
            else
            {
                float rngVal = UnityEngine.Random.value;
                if (rngVal < MutateGlobal.Instance.plantAddChance)
                {
                    result.geneCode += MutateGlobal.Instance.GetRandomGeneCodeLetter();//add a random letter
                    result.geneCode += source.geneCode[i];//before putting the next one in
                }
                else if (rngVal <  MutateGlobal.Instance.plantDeleteChance + MutateGlobal.Instance.plantAddChance)
                {
                    //do nothing, ie, skip the next letter
                }
                else if (rngVal < MutateGlobal.Instance.plantDeleteChance + MutateGlobal.Instance.plantAddChance + MutateGlobal.Instance.plantReplaceChance)
                {
                    result.geneCode += MutateGlobal.Instance.GetRandomGeneCodeLetter();//add a random letter
                    //and skip next letter, therby replacing
                }
            }
        }
        if(result.geneCode == "")
        {
            result.geneCode += 'F';
        }
        result.geneCode = PlantGeneticsConstraintSolver(result.geneCode);

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

    private static String PlantGeneticsConstraintSolver(String Code)
    {
        String FinalCode = Code;
        int Stalkcount = 0;
        for(int i = 0; i < Code.Length; i++)
        {
            if(Code[i] == 'S')
            {
                Stalkcount++;
            }
            if(Code[i] == 'F')
            {
                break;
            }
        }
        while(Stalkcount < 3)
        {
            Stalkcount++;
            FinalCode = "S" + FinalCode;
        }
        if(!FinalCode.EndsWith('F'))
        {
            FinalCode += "F"; //append an F if there isn't one at the ends
        }

        return FinalCode;
        //edit Code so that it fits requirements (Flower at the end, x leaves per segment and so on)
    }
}
