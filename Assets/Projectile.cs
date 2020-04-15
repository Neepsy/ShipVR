using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public GameObject splashParticles;
    public GameObject explodeParticles;
    public int damageDealt = 1;

    

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

            Target tg = hit.GetComponent<Target>();
            tg?.damage(damageDealt);
            
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
