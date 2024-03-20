using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WindMotion))]
public class WindDisplay : MonoBehaviour
{
    WindMotion wm;

    // Start is called before the first frame update
    void Start()
    {
        wm = GetComponent<WindMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + wm.CalculateForce(), Color.green);
    }

}
