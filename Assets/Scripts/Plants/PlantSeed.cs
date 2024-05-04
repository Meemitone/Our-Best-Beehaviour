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
    private float energyInterval = 0.5f;
    [SerializeField] public FlowerData genetics;
    [SerializeField] Rigidbody myRB;
    [SerializeField] GameObject model;
    public bool debugMutate = false;
    List<int> LeafPivots;
    public List<PlantPart> parts;
    Coroutine currentGrow;
    private bool isDying = false;
    public int Generation = 0;
    // Start is called before the first frame update
    void Start()
    {
        energy = genetics.energy;
        LeafPivots = new List<int>();
        for (int j = 0; j < 6; j++)
            LeafPivots.Add(j);
        StartCoroutine(Energy());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myRB != null && transform.position.y < 2f && myRB.velocity.sqrMagnitude < 0.01f)
        {
            model.transform.localRotation = transform.rotation;
            transform.rotation = Quaternion.identity;
            StartCoroutine(GrowPlant());
            Destroy(myRB);
        }

        if(myRB!=null && transform.position.y < -10f)
        {
            Destroy(gameObject);
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

    public IEnumerator Die()
    {
        if (isDying)
            yield break;
        isDying = true;
        if (currentGrow != null)
            StopCoroutine(currentGrow);
        StopCoroutine(GrowPlant());
        for (int i = parts.Count - 1; i >= 0; i--)
        {
            yield return StartCoroutine(parts[i].Ungrow());
            parts.RemoveAt(i);
        }
        Destroy(gameObject);
        yield return null;
    }

    IEnumerator Energy()
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        while (true)
        {
            float cost = 0f;
            foreach (PlantPart part in parts)
            {
                cost += part.GetUpkeep();
            }
            energy -= cost;
            if (energy <= 0)
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
        for (int i = 0; i < genetics.geneCode.Length; i++)
        {
            char GeneLetter = genetics.geneCode[i];
            switch (GeneLetter)
            {
                case 'S':
                    if (currentSegment == null)
                    {
                        GameObject newSegment = Instantiate(Segment, transform);
                        currentSegment = newSegment.GetComponent<Stalk>();
                        parts.Add(newSegment.GetComponent<PlantPart>());
                        currentGrow = StartCoroutine(parts[parts.Count - 1].Grow());
                        yield return currentGrow;
                    }
                    else
                    {
                        GameObject newSegment = Instantiate(Segment, currentSegment.nextPivot.transform);
                        newSegment.transform.localRotation = currentSegment.transform.localRotation;
                        float previousMaxScale = currentSegment.maxScale;
                        currentSegment = newSegment.GetComponent<Stalk>();
                        currentSegment.maxScale = previousMaxScale * genetics.segmentSizeRatio;
                        parts.Add(newSegment.GetComponent<PlantPart>());
                        currentGrow = StartCoroutine(parts[parts.Count - 1].Grow());
                        yield return currentGrow;
                    }
                    break;
                case 'L':
                    if (currentSegment == null)
                    {
                        //Debug.LogWarning("PlantGenetics Should not start with 'L'");
                    }
                    else
                    {//Will grow Leaves on a random Leaf Pivot, won't grow a seventh
                        ShuffleIntList(LeafPivots);
                        foreach (int p in LeafPivots)
                        {
                            if (currentSegment.otherPivot.transform.GetChild(p).childCount == 0)
                            {
                                GameObject target = currentSegment.otherPivot.transform.GetChild(p).gameObject;
                                GameObject newLeaf = Instantiate(Leaf, target.transform);
                                parts.Add(newLeaf.GetComponent<PlantPart>());
                                currentGrow = StartCoroutine(parts[parts.Count - 1].Grow());
                                yield return currentGrow;
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
                        currentGrow = StartCoroutine(parts[parts.Count - 1].Grow());
                        yield return currentGrow;
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
