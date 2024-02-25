using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantPart : MonoBehaviour //Abstract means that this should be finished by classes that inherit from it, and not instantiated alone
{
    [SerializeField] internal float linearScale = 0.01f; //size of the object
    [SerializeField] internal float startScale = 0.01f;
    [SerializeField] internal float growthRate = 1f;
    [SerializeField] internal float maxScale = 1f;
    [SerializeField] internal float growCostScale = 1f;
    [SerializeField] internal bool isGrowing = true;

    [SerializeField] internal float upkeepCostScale = 1f; //base cost of a scale 1 object per second

    public virtual float GetUpkeep()//A virtual has a default implementation, but can be overriden by an inheriting class
    {
        float cost = -upkeepCostScale * linearScale * Time.deltaTime * (isGrowing ? growCostScale: 0f);
        //should be negative unless leaf makes energy
        return cost;
    }

    // Start is called before the first frame update
    internal virtual void Start()
    {
        if(!isGrowing)
        {
            linearScale = maxScale;
        }
        else
        {
            linearScale = startScale;
        }
    }

    // Update is called once per frame
    internal virtual void Update()
    {
        if(isGrowing)
        {
            linearScale += growthRate * Time.deltaTime;
            if(linearScale >= maxScale)
            {
                linearScale = maxScale;
                isGrowing = false;
            }
            transform.localScale = new Vector3(1, 1, 1) * linearScale;
        }
    }
}
