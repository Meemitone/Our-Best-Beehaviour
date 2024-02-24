using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StalkSway : MonoBehaviour
{
    [SerializeField] internal GameObject nextPivot;
    [SerializeField] internal GameObject otherPivot;
    [SerializeField] internal StalkSway nextStalk;
    [SerializeField] internal StalkSway prevStalk;
    [SerializeField] private bool Root, Head;
    [SerializeField]
    public bool root
    {
        get { return Root; }
        set { Root = value; }
    }

    [SerializeField]
    public bool head
    {
        get { return Head; }
        set
        {
            Head = value;
            segmentCount = 0; //if I suddenly stop being the head, or become the head, chance is, segment count has changed setting it to zero has it recalculate
            if (!Head)
            {
                if (swayMotion != null)
                {
                    StopCoroutine(swayMotion);
                    swayMotion = null;
                }
            }
        }
    }
    [SerializeField] internal Quaternion sway;
    [SerializeField] internal float swayRange = 30f;
    private Coroutine swayMotion;
    private float swayTime;
    [SerializeField] internal float swayTimeRangeMin = 0.5f;
    [SerializeField] internal float swayTimeRangeMax = 1f;
    [SerializeField] internal float dampening = 0.5f;

    [SerializeField] int segmentCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        sway = new Quaternion();
        if (!root)
        {
            if (transform.parent != null && transform.parent.parent != null)
            {
                if (transform.parent.parent.TryGetComponent<StalkSway>(out prevStalk))
                {
                    prevStalk.nextStalk = this;
                    prevStalk.head = false;
                }
                else
                {
                    root = true;
                }
            }
            else
                root = true;
        }

    }

    private void Update()
    {
        if (!head)
        {
            if(nextStalk!=null)
            transform.localRotation =  Quaternion.Lerp(transform.localRotation, nextStalk.transform.localRotation, dampening*Time.deltaTime);
            else
            {
                head = true;
            }
        }
        else
        {
            if (swayMotion == null)
            {
                if(segmentCount <= 0)
                {
                    segmentCount = CountSegments();
                }
                float localSwayRange = swayRange/segmentCount;
                sway.eulerAngles = new Vector3(Random.Range(-localSwayRange, localSwayRange), 0, Random.Range(-localSwayRange, localSwayRange));
                swayTime = Random.Range(swayTimeRangeMin, swayTimeRangeMax);
                swayMotion = StartCoroutine(SwayCoroutine());
            }
        }
    }

    private int CountSegments()
    {
        int count = 0;
        StalkSway pointer = this;
        do
        {
            count++;
            pointer = pointer.prevStalk;
        }
        while (pointer != null);
        return count;
    }

    IEnumerator SwayCoroutine()
    {
        Quaternion start = transform.localRotation;
        Quaternion end = sway;
        float time = swayTime;
        float timer = 0f;
        float d; //between -1 and 1
        do
        {
            timer += Time.deltaTime;
            d = Mathf.Cos(Mathf.PI*(1 - timer/time));
            d = d + 1;
            d = d / 2;
            transform.localRotation = Quaternion.Slerp(start, end, d);

            yield return null;
        }
        while (timer < time);
        transform.localRotation = end;
        swayMotion = null;
        yield break;
    }

    
}
