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
