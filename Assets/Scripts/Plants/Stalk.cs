using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stalk : PlantPart
{
    [SerializeField] internal GameObject nextPivot;
    [SerializeField] internal GameObject otherPivot;
    [SerializeField] internal Stalk nextStalk;
    [SerializeField] internal Stalk prevStalk;
    [SerializeField] private bool Root, Head;
    public bool root
    {
        get { return Root; }
        set { Root = value; }
    }

    public bool head
    {
        get { return Head; }
        set
        {
            Head = value;
            segmentCount = 0; //if I suddenly stop being the head, or become the head, chance is, segment count has changed setting it to zero has it recalculate
        }
    }
    [SerializeField] internal Quaternion sway;
    [SerializeField] internal float swayRange = 30f;
    //private Coroutine swayMotion;
    private float swayTime;
    [SerializeField] internal float swayTimeRangeMin = 0.5f;
    [SerializeField] internal float swayTimeRangeMax = 1f;
    [SerializeField] internal float dampening = 0.5f;

    [SerializeField] int segmentCount = 0;

    private Behaviour[] behaviours;

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        behaviours = GetComponents<Behaviour>();
        sway = new Quaternion();
        if (!root)
        {
            if (transform.parent != null && transform.parent.parent != null)
            {
                if (transform.parent.parent.TryGetComponent(out prevStalk)) //TryGetComponent will try for a "this" by default
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


    internal void FixedUpdate()
    {
        if (!head)
        {
            if (nextStalk != null)
                transform.localRotation = Quaternion.Slerp(transform.localRotation, nextStalk.transform.localRotation, dampening * Time.deltaTime);
            else
            {
                head = true;
            }
        }
        else
        {
            if (segmentCount <= 0)
            {
                segmentCount = CountSegments();
            }
            Vector3 forceAcc = Vector3.zero;
            foreach (Behaviour b in behaviours)
            {
                if(b.active)
                forceAcc += b.CalculateForce();
            }
            Vector3 forceAccZ = forceAcc;
            Vector3 forceAccX = forceAcc;
            forceAccZ.x = 0;
            forceAccX.z = 0;
            float xAngle = Vector3.Angle(Vector3.up, forceAccX);
            float zAngle = Vector3.Angle(Vector3.up, forceAccZ);

            xAngle = Mathf.Clamp(xAngle, 0, swayRange);
            zAngle = Mathf.Clamp(zAngle, 0, swayRange);

            sway.eulerAngles = new Vector3(forceAccZ.z < 0 ? -zAngle / segmentCount : zAngle / segmentCount, 0, forceAccX.x > 0 ? -xAngle / segmentCount : xAngle / segmentCount);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, sway, dampening * Time.deltaTime);

            /*
            float localSwayRange = swayRange / segmentCount;
            sway.eulerAngles = new Vector3(Random.Range(-localSwayRange, localSwayRange), 0, Random.Range(-localSwayRange, localSwayRange));
            swayTime = Random.Range(swayTimeRangeMin, swayTimeRangeMax);
            swayMotion = StartCoroutine(SwayCoroutine());
            */

        }
    }

    private int CountSegments()
    {
        int count = 0;
        Stalk pointer = this;
        do
        {
            count++;
            pointer = pointer.prevStalk;
        }
        while (pointer != null);
        return count;
    }


    ////This'll be depreciated when actual motion happens with wind and/or sun
    //IEnumerator SwayCoroutine()
    //{
    //    Quaternion start = transform.localRotation;
    //    Quaternion end = sway;
    //    float time = swayTime;
    //    float timer = 0f;
    //    float d; //between -1 and 1
    //    do
    //    {
    //        timer += Time.deltaTime;
    //        d = Mathf.Cos(Mathf.PI * (1 - timer / time));
    //        d = d + 1;
    //        d = d / 2;
    //        transform.localRotation = Quaternion.Slerp(start, end, d);

    //        yield return null;
    //    }
    //    while (timer < time);
    //    transform.localRotation = end;
    //    swayMotion = null;
    //    yield break;
    //}


}
