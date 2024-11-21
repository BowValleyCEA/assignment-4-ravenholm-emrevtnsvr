using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private float Distance = 5;
    
    public bool isTaked = false;

    public Transform Pivot;

    GameObject Item;

   
    void Update()
    {
        if (isTaked == true)
        {
            var objComp = Item.GetComponent<Object>();
            if (objComp.taken == false)
            {
                isTaked = false;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                objComp.Force();
                isTaked = false;
                objComp.taken = false;
            }

        }

        if (isTaked == false)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, Distance) && hit.collider.gameObject.tag == "takeAble")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                   
                    Item = hit.collider.gameObject;
                    Item.GetComponent<Object>().hasReseted = false;
                    Item.GetComponent<Object>().taken = true;
                    isTaked = true;
                    
                    
                    
                }

               
            }
        } 
    }
}
