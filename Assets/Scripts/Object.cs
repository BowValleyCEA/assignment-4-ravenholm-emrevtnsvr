using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Object : MonoBehaviour
{
    Rigidbody rb;
    public bool taken = false;

    [HideInInspector]
    public bool hasReseted = false;
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (taken == true)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("pivot");

            rb.MovePosition(obj.transform.position);
            rb.MoveRotation(Quaternion.LookRotation(Camera.main.transform.forward));

            rb.useGravity = false;


        }
        else
        {
            rb.useGravity = true;
        }
        if(hasReseted = false)
        {
            resetForce();
            hasReseted = true;
        }    
    } 
    public void resetForce()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
    }

    public void Force()
    {
        rb.AddForce(Camera.main.transform.forward, ForceMode.Impulse);
    }
  
    private void OnCollisionEnter(Collision collision)
    {
        taken = false;
    }
}
