using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject Segment;
    [SerializeField] GameObject Leaf;
    [SerializeField] GameObject Flower;
    [Header("")]
    public float energy;
    private float energyInterval = 1f;
    [SerializeField] public FlowerData genetics;
    [SerializeField] Rigidbody myRB;
    [SerializeField] GameObject model;
    public bool debugMutate = false;
    List<int> LeafPivots;
    public List<PlantPart> parts;

    // Start is called before the first frame update
    void Start()
    {
        energy = genetics.energy;
        LeafPivots = new List<int>();
        for (int j = 0; j < 6; j++)
            LeafPivots.Add(j);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(myRB!=null && transform.position.y < 2f && myRB.velocity.sqrMagnitude < 0.01f)
        {
            model.transform.localRotation = transform.rotation;
            transform.rotation = Quaternion.identity;
            StartCoroutine(GrowPlant());
            Destroy(myRB);
        }

        if (debugMutate)
        {
            debugMutate = false;
            genetics = FlowerData.Copy(genetics);
            StopAllCoroutines();
            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(GrowPlant());
        }
    }

    IEnumerator Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
        yield return null;
    }

    IEnumerator Energy()
    {
        yield return new WaitForSeconds(Random.Range(0,1f));
        while(true)
        {
            float cost = 0f;
            foreach(PlantPart part in parts)
            {
                cost += part.GetUpkeep();
            }
            energy -= cost;
            if(energy <= 0)
            {
                StartCoroutine(Die());
                yield break;
            }
            yield return new WaitForSeconds(energyInterval);
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
                        parts.Add(newSegment.GetComponent<PlantPart>());
                        yield return StartCoroutine(currentSegment.Grow());
                    }
                    else
                    {
                        GameObject newSegment = Instantiate(Segment, currentSegment.nextPivot.transform);
                        newSegment.transform.localRotation = currentSegment.transform.localRotation;
                        float previousMaxScale = currentSegment.maxScale;
                        currentSegment = newSegment.GetComponent<Stalk>();
                        currentSegment.maxScale = previousMaxScale * genetics.segmentSizeRatio;
                        parts.Add(newSegment.GetComponent<PlantPart>());
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
                                parts.Add(newLeaf.GetComponent<PlantPart>());
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
                        newFlower.GetComponent<Flower>().myGenes = genetics;
                        newFlower.GetComponent<Flower>().mySeed = this;
                        parts.Add(newFlower.GetComponent<PlantPart>());
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
