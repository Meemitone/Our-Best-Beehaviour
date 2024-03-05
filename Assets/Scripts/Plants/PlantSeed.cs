using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject Segment;
    [SerializeField] GameObject Leaf;
    [SerializeField] GameObject Flower;


    [SerializeField] FlowerData genetics;

    public bool debugMutate = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GrowPlant());
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

    IEnumerator GrowPlant()
    {
        StalkSway currentSegment = null;
        for(int i = 0; i < genetics.geneCode.Length; i++)
        {
            char GeneLetter = genetics.geneCode[i];
            switch(GeneLetter)
            {
                case 'S':
                    if(currentSegment == null)
                    {
                        GameObject newSegment = Instantiate(Segment, transform);
                        currentSegment = newSegment.GetComponent<StalkSway>();
                        yield return StartCoroutine(currentSegment.Grow());
                    }
                    else
                    {
                        GameObject newSegment = Instantiate(Segment, currentSegment.nextPivot.transform);
                        float previousMaxScale = currentSegment.maxScale;
                        currentSegment = newSegment.GetComponent<StalkSway>();
                        currentSegment.maxScale = previousMaxScale * genetics.segmentSizeRatio;
                        yield return StartCoroutine(currentSegment.Grow());
                    }
                    break;
                case 'L':
                    if(currentSegment == null)
                    {
                        Debug.LogWarning("PlantGenetics Should not start with 'L'");
                    }
                    else
                    {
                        //get random pivot from the OtherPivots Group
                        //instantiate a leaf childed to that
                        //don't double up
                    }
                    break;
                case 'F':
                    
                    break;
                default:
                    Debug.LogError("GrowPlant not expecting a '" + GeneLetter + "'");
                    break;
            }
        }

        yield return null;
    }
}
