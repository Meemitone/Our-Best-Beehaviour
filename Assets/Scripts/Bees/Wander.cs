using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Wander : Behaviour
    {

        bool resetWander = true;

        [SerializeField] float resetTimer = 0.5f;

        private Vector3 currentCalForce = new Vector3();
        private Vector3 nextCalForce = new Vector3();

        float currentTime;

        public override Vector3 CalculateForce()
        {
            if (resetWander)
            {
                nextCalForce = new Vector3(Random.Range(-weight, weight), Random.Range(-weight, weight), Random.Range(-weight, weight));
                nextCalForce = Vector3.ClampMagnitude(nextCalForce, weight);
                currentTime = 0;
                StartCoroutine(ResetWander());
            }

            currentTime += Time.deltaTime;

            currentCalForce = Vector3.Slerp(currentCalForce, nextCalForce, currentTime / resetTimer);

            return currentCalForce;
        }

        private IEnumerator ResetWander()
        {
            resetWander = false;

            yield return new WaitForSeconds(resetTimer);

            resetWander = true;
        }
    }
