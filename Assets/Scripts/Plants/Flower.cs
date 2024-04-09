using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : PlantPart
{
    [SerializeField] GameObject[] seedPoints;
    public float pollen = 0f;
    private float pollenLimit = 1f;
    public FlowerData myGenes;
    public List<FlowerData> collectedGenes;
    private Coroutine currentBehaviour;

    public override IEnumerator Grow()
    {
        yield return base.Grow();
        currentBehaviour = StartCoroutine(Pollenate());
        collectedGenes.Add(myGenes);
        collectedGenes.Add(myGenes);
        collectedGenes.Add(myGenes);
    }

    private IEnumerator Pollenate()
    {
        yield return null;
        pollen += Time.deltaTime * 0.05f;
        pollen = Mathf.Min(pollen, pollenLimit);
    }

    public float TakePollen(float max, ref FlowerData mostRecent)
    {
        float retVal = pollen;
        if (mostRecent !=null)
        {
            collectedGenes.Add(FlowerData.Copy(mostRecent, 0, false));
        }
        mostRecent = myGenes;
        if(max > pollen)
        {
            retVal = pollen;
            pollen = 0;
        }
        else
        {
            pollen -= max;
            retVal = max;
        }

        if(collectedGenes.Count > 5)
        {
            pollen = 0;
            StopCoroutine(currentBehaviour);
            currentBehaviour = StartCoroutine(Seeding());
        }

        return retVal;
    }

    private IEnumerator Seeding()
    {
        yield break;
    }
}
