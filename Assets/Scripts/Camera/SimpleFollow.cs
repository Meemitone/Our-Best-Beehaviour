using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField] Transform beeFollow;

    private void FixedUpdate()
    {

        transform.position = beeFollow.position;

    }
}
