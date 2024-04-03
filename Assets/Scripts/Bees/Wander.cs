using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bees
{
    public class Wander : Behaviour
    {

        bool resetWander = true;

        [SerializeField] float resetTimer = 0.5f;

        private Vector3 currentCalForce = new Vector3();

        public override Vector3 CalculateForce()
        {
            if (resetWander)
            {
                currentCalForce = new Vector3(Random.Range(-weight, weight), Random.Range(-weight, weight), weight);
                currentCalForce = Vector3.ClampMagnitude(currentCalForce, weight);
                StartCoroutine(ResetWander());
            }

            return currentCalForce;
        }

        private IEnumerator ResetWander()
        {
            resetWander = false;

            yield return new WaitForSeconds(resetTimer);

            resetWander = true;
        }
    }
}