using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject splashParticles;
    public GameObject explodeParticles;
    public bool piercesArmor;

    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject hit = collision.gameObject;
        if (hit.tag.Equals("Water"))
        {
            if(splashParticles != null)
            {
                Destroy(Instantiate(splashParticles, transform.position, Quaternion.identity), 10f);
            }
        }
        else
        {
            if(explodeParticles != null)
            {
                Destroy(Instantiate(explodeParticles, transform.position, Quaternion.identity), 10f);
            }
        }

        Destroy(gameObject);
    }

    public virtual void detonate()
    {
        if (explodeParticles != null)
        {
            Destroy(Instantiate(explodeParticles, transform.position, Quaternion.identity), 10f);
        }
    }
}
