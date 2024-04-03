using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Bees
{
    public class BeeBoid : BoidBasic
    {

        public float turnSpeed = 0.2f;
        public float rotaSpeed = 0.2f;

        public Vector3 currentDirection;

        public float turnMax;

        private void FixedUpdate()
        {

            Movement();
            Rotation();

        }

        public void Movement()
        {

            currentDirection = Vector3.RotateTowards(currentDirection, force, turnSpeed, turnSpeed);
            transform.Translate(currentDirection);

        }

        public void Rotation()
        {
            //transform.rotation = Quaternion.LookRotation(currentDirection.normalized);

            Quaternion newRotation = Quaternion.LookRotation(currentDirection.normalized);

            Vector2 rotations = new Vector2(force.normalized.x - currentDirection.normalized.x, force.normalized.y - currentDirection.normalized.y);

            newRotation = Quaternion.Euler(rotations.y * turnMax, newRotation.eulerAngles.y, rotations.x * turnMax);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotaSpeed);
            
        }

    }
}