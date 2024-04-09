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
    List<int> LeafPivots;

    // Start is called before the first frame update
    void Start()
    {
        LeafPivots = new List<int>();
        for (int j = 0; j < 6; j++)
            LeafPivots.Add(j);
        StartCoroutine(GrowPlant()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (debugMutate)
        {
            debugMutate = false;
            genetics = FlowerData.Copy(genetics);
            StopAllCoroutines();
            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(GrowPlant());
        }
    }

    IEnumerator GrowPlant()
    {
        Stalk currentSegment = null;
        for(int i = 0; i < genetics.geneCode.Length; i++)
        {
            char GeneLetter = genetics.geneCode[i];
            switch(GeneLetter)
            {
                case 'S':
                    if(currentSegment == null)
                    {
                        GameObject newSegment = Instantiate(Segment, transform);
                        currentSegment = newSegment.GetComponent<Stalk>();
                        yield return StartCoroutine(currentSegment.Grow());
                    }
                    else
                    {
                        GameObject newSegment = Instantiate(Segment, currentSegment.nextPivot.transform);
                        newSegment.transform.localRotation = currentSegment.transform.localRotation;
                        float previousMaxScale = currentSegment.maxScale;
                        currentSegment = newSegment.GetComponent<Stalk>();
                        currentSegment.maxScale = previousMaxScale * genetics.segmentSizeRatio;
                        yield return StartCoroutine(currentSegment.Grow());
                    }
                    break;
                case 'L':
                    if(currentSegment == null)
                    {
                        //Debug.LogWarning("PlantGenetics Should not start with 'L'");
                    }
                    else
                    {//Will grow Leaves on a random Leaf Pivot, won't grow a seventh
                        ShuffleIntList(LeafPivots);
                        foreach (int p in LeafPivots)
                        {
                            if(currentSegment.otherPivot.transform.GetChild(p).childCount == 0)
                            {
                                GameObject target = currentSegment.otherPivot.transform.GetChild(p).gameObject;
                                GameObject newLeaf = Instantiate(Leaf, target.transform);
                                yield return StartCoroutine(newLeaf.GetComponent<PlantPart>().Grow());
                                break;
                            }
                        }
                    }
                    break;
                case 'F':
                    if (currentSegment == null)
                    {
                        break;
                        //Debug.LogWarning("PlantGenetics Should not start with 'F'");
                    }
                    else
                    {
                        GameObject newFlower = Instantiate(Flower, currentSegment.nextPivot.transform);
                        yield return StartCoroutine(newFlower.GetComponent<PlantPart>().Grow());
                        yield break;
                    }
                default:
                    Debug.LogError("GrowPlant not expecting a '" + GeneLetter + "'");
                    break;
            }
        }

        yield break;
    }


    private void ShuffleIntList(List<int> inputList)
    {    //take any list of GameObjects and return it with Fischer-Yates shuffle
        int i = 0;
        int t = inputList.Count;
        int r;
        int p;

        while (i < t - 1)
        {
            r = Random.Range(i, inputList.Count);
            p = inputList[i];
            inputList[i] = inputList[r];
            inputList[r] = p;
            i++;
        }
    }
}
