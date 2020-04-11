using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DepthCharge : MonoBehaviour
{
    //particles for hitting water
    public GameObject smallSplash;
    //particles for exploding
    public GameObject explodeSplash;
    public int lifetimeFrames = 600;
    public DepthChargeSpawner spawner;
    private bool armed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (armed)
        {
            lifetimeFrames--;
            if(lifetimeFrames == 0)
            {
                selfDestruct();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("DC"))
        {
            detonate();
        }
    }

    public void spawnNextCharge()
    {
        spawner.beginCountdown();
    }

    public void arm()
    {
        armed = true;
    }

    private void selfDestruct()
    {
        Destroy(gameObject);
    }

    private void detonate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, .8f);
        foreach (Collider hit in hits)
        {
            //check for possibility that the collider was already destroyed
            if (hit != null)
            {
                //destroy all submarines in detonation radius
                if (hit.gameObject.tag.Equals("Sub"))
                {
                    Debug.Log("destroying");
                    Destroy(hit.gameObject);
                }
            }
        }

        if(explodeSplash != null)
        {
            Vector3 splashPos = new Vector3(transform.position.x, 0, transform.position.z);
            Destroy(Instantiate(explodeSplash, splashPos, Quaternion.identity), 5);
        }

        Debug.Log("BOOOM!");
        //Destroy(gameObject);

    }

}
