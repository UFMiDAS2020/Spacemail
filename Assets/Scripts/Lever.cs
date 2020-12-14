﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    Vector3 initPos;
    Quaternion initRot, downRot;
    Rigidbody rb;
    BoxCollider collider;
    float minAngle, maxAngle, deltaAngle, rotationSpeed;
    bool resetting;

    private void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        minAngle = 280;
        maxAngle = 340;
        deltaAngle = 0;
        rotationSpeed = 10f;
    }
    private void FixedUpdate()
    {
        Vector3 currRot = transform.rotation.eulerAngles;
        if (resetting)
        {
            Debug.Log("Resetting! " + currRot);
            if (currRot.x >= maxAngle)
            {
                Debug.Log("Stopped resetting!");
                collider.enabled = true;
                rb.angularVelocity = Vector3.zero;
                resetting = false;
            }
            else
            {
                collider.enabled = false;
                deltaAngle += rotationSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, maxAngle);
                transform.rotation = downRot * Quaternion.AngleAxis(deltaAngle, Vector3.right);
            }
        }
        else
        {
            if (currRot.x < minAngle || maxAngle < currRot.x)
            {
                if (currRot.x < minAngle)
                {
                    //Debug.Log("Moving to " + minAngle + " from " + currRot.x + "!");
                    currRot.x = minAngle;
                    resetting = true;
                    deltaAngle = 0;
                }
                else if (maxAngle < currRot.x)
                {
                    //Debug.Log("Moving to " + maxAngle + " from " + currRot.x + "!");
                    currRot.x = maxAngle;
                }
                currRot.y = initRot.y;
                currRot.z = initRot.z;
                transform.rotation = Quaternion.Euler(currRot.x, currRot.y, currRot.z);
                if (resetting) downRot = transform.rotation;
            }
        }
        transform.position = initPos;
    }
}