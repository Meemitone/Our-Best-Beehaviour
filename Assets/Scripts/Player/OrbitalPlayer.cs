using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalPlayer : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    public int index;
    [SerializeField] private bool isActive;
    private GameObject target;


    public void SwitchBee(bool left)
    {
        isActive = true;
        cam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        BeeBoid[] boids = FindObjectsOfType<BeeBoid>();
        
        if (left)
        {
            index--;
            if (index < 0)
            {
                index = boids.Length -1;
            }
        }
        else
        {
            index++;
        }

        target = boids[index].gameObject;
    }

    public void Deactivate()
    {
        isActive = false;
        GetComponentInChildren<Camera>().gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
    
    private void Update()
    {
        if (isActive)
        {
            Vector2 lookVector = Input.mousePosition;
            transform.RotateAround(Vector3.up, Input.GetAxis("Mouse X") * 0.01f);
            transform.RotateAround(transform.right,  Input.GetAxis("Mouse Y") * 0.01f);

            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (target != null)
            {
                transform.position = target.transform.position;
            }
        }


        if (Input.mouseScrollDelta.y > 0.1)
        {
            cam.transform.localPosition += Vector3.forward;
        }
        else if (Input.mouseScrollDelta.y < -0.1)
        {
            cam.transform.localPosition -= Vector3.forward;
        }
    }
}
