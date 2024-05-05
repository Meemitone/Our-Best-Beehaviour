using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Behaviour
{

    bool resetWander = true;

    [SerializeField] float resetTimer = 0.5f;

    private Vector3 currentCalForce = new Vector3();
    private Vector3 nextCalForce = new Vector3();

    public float wanderAmount = 1f;
    public float wanderY = 0.2f;


    float currentTime;

    private void Awake()
    {

        currentCalForce = transform.forward;
        nextCalForce = transform.forward;

    }

    public override Vector3 CalculateForce()
    {
        if (resetWander)
        {
            nextCalForce = (transform.forward + new Vector3(Random.Range(-wanderAmount, wanderAmount), Random.Range(-0.2f, 0.2f) - transform.forward.y, Random.Range(-wanderAmount, wanderAmount))) * weight;
            nextCalForce = Vector3.ClampMagnitude(nextCalForce, weight);
            currentTime = 0;
            StartCoroutine(ResetWander());
        }

        currentTime += Time.deltaTime;

        currentCalForce = Vector3.Lerp(currentCalForce, nextCalForce, currentTime / resetTimer);

        return currentCalForce;
    }

    private IEnumerator ResetWander()
    {
        resetWander = false;

        yield return new WaitForSeconds(resetTimer);

        resetWander = true;
    }
}
