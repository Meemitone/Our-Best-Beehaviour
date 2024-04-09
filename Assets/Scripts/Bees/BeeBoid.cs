using System.Collections;
using UnityEngine;
using System;

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

            currentDirection = currentDirection + force * Time.deltaTime;

            currentDirection = Vector3.ClampMagnitude(currentDirection, maxForce);

            transform.Translate(currentDirection);

        }

        public void Rotation()
        {

            Vector3 currentUp = CalBank();

            Vector3 currentFor = CalForward(currentUp);

            if (currentDirection.magnitude > 0)
            {
                Quaternion newRotation = Quaternion.LookRotation(currentFor, currentUp);

                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);

            }
            
        }

        private Vector3 CalBank()
        {

            Vector3 calculateUp = gravity * Vector3.up;

            calculateUp += force;

            return calculateUp.normalized;

        }

        private Vector3 CalForward(Vector3 up)
        {

            Vector3 right = Vector3.Cross(currentDirection, up);

            Vector3 forward = Vector3.Cross(up, right);

            return forward;

        }

    }
