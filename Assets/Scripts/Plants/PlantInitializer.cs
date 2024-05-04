using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInitializer : MonoBehaviour
{
    public PlantSeed seed;
    // Start is called before the first frame update
    void Start()
    {

        string randCode = "";
        int codeLength = Random.Range(10, 20);
        for(int i = 0; i < codeLength; i++)
        {
            randCode += MutateGlobal.instance.GetRandomGeneCodeLetter();
        }
        randCode = FlowerData.PlantGeneticsConstraintSolver(randCode);
        seed.genetics.geneCode = randCode;
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
