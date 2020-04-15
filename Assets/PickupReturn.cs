using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupReturn : MonoBehaviour
{
    public Transform parent;
    private Rigidbody rb;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if(parent != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.position = parent.position;
            gameObject.transform.rotation = parent.rotation;
        }
    }

    public void Pickup()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void returnPickup()
    {
        if(parent != null)
        {
            gameObject.transform.parent = parent;
            gameObject.transform.position = parent.position;
            gameObject.transform.rotation = parent.rotation;

            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }
}
