using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Missile : Projectile
{
    public GameObject target;
    public float thrust = 10f;

    //number of frames that have elapsed since the object spawned. Used to determinte what phase the missile is in
    //(launch, rotate, terminal)
    private int phaseFrame;
    public int rotateFrames = 120;
    public int launchFrames = 60;
    private bool terminalStarted = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        phaseFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        phaseFrame++;

        if(target == null)
        {
            //backup
            target = GameObject.FindGameObjectWithTag("Target");
        }
    }

    public void setTarget(GameObject obj)
    {
        target = obj;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.tag.Equals("Water"))
        {
            if (splashParticles != null)
            {
                Destroy(Instantiate(splashParticles, transform.position, Quaternion.identity), 10f);
            }
        }
        else
        {
            if (explodeParticles != null)
            {
                Destroy(Instantiate(explodeParticles, transform.position, Quaternion.identity), 10f);
            }
        }
        detonate();
    }

    public override void detonate()
    {
        //use overlap sphere to check for targets to destory
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.6f);
        foreach (Collider hit in hits)
        {

            //check for possibility that the collider was already destroyed
            if (hit != null)
            {
                if (hit.gameObject.tag.Equals("Target"))
                {
                    Destroy(hit.gameObject);
                }
            }
        }

        Destroy(gameObject);
    }


    void FixedUpdate()
    {   
        if(phaseFrame > (launchFrames + rotateFrames))
        {
            if (!terminalStarted)
            {
                //its not realistic, but otherwise the missile will just forever orbit the target
                terminalStarted = true;
                rb.velocity = Vector3.zero;
            }
            //fly straight at the target
            transform.LookAt(target.transform.position);
        }
        else if(phaseFrame > launchFrames)
        {
            //slerp towards looking at the target
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (phaseFrame - launchFrames) / (float) rotateFrames);
        }

        //missile always has forwards thrust
        rb.AddForce(transform.forward * thrust);

        //check if in range
        if(Vector3.Distance(transform.position, target.transform.position) <= 0.4f)
        {
            detonate();
     
        }
    }
}
