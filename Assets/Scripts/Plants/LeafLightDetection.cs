using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafLightDetection : MonoBehaviour
{
    [SerializeField] GameObject center;
    [SerializeField] GameObject[] radials;
    [SerializeField] float maxCastDist;

    [SerializeField] bool debug = false; //draw rays and update ratioDisplay each frame if true
    [SerializeField] bool debugPrint = false; //print colliders hit by central ray if true
    [SerializeField] float ratioDisplay = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (debug)
            ratioDisplay = GetLightRatio();
    }

    float GetLightRatio()
    {
        Quaternion lightDirection = EnvironmentalSunlight.instance.transform.rotation;

        Vector3 LD = lightDirection * Vector3.forward;
        LD = -LD;

        RaycastHit[] radialCasts = new RaycastHit[radials.Length];
        RaycastHit centralCast = new RaycastHit();

        Physics.Raycast(center.transform.position, LD, out centralCast, maxCastDist, Physics.AllLayers, QueryTriggerInteraction.Collide);
        if (debug)
            Debug.DrawRay(center.transform.position, LD, centralCast.collider == null ? Color.red : Color.blue);
        if (debugPrint)
        {
            if (centralCast.collider != null)
            {
                Debug.Log(centralCast.collider);
            }
        }
        for (int i = 0; i < radials.Length; i++)
        {
            Physics.Raycast(radials[i].transform.position, LD, out radialCasts[i], maxCastDist, Physics.AllLayers, QueryTriggerInteraction.Collide);
            if (debug)
                Debug.DrawRay(radials[i].transform.position, LD, radialCasts[i].collider == null ? Color.red : Color.blue);
        }

        float blockedCount = 0;
        for (int i = 0; i < radialCasts.Length; i++)
        {
            if (radialCasts[i].collider != null)
            {
                int next = i + 1;
                int doubleNext = i + 2;
                next %= radialCasts.Length;
                doubleNext %= radialCasts.Length;
                if (radialCasts[next].collider != null)
                    blockedCount++;
                if (radialCasts[doubleNext].collider != null)
                    blockedCount++;
                if (centralCast.collider != null)
                    blockedCount++;
            }
            else
            {
                if (centralCast.collider != null)
                    blockedCount++;
            }
        }

        return 1 - (blockedCount / ((float)radials.Length * 3));
    }
}
