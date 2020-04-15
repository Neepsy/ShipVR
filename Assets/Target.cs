using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{

    public int health = 1;
    public GameObject deathParticles;
    public delegate void targetDestroyedEvent(Target t);
    public event targetDestroyedEvent onDestroyed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(int damageTaken)
    {
        health -= damageTaken;

        if(health <= 0)
        {
            //only invoke if event is not null
            onDestroyed?.Invoke(this);

            if (deathParticles != null)
            {
                Destroy(Instantiate(deathParticles, transform.position, Quaternion.identity), 10f);
            }

            Destroy(this.gameObject);
        }
    }
}
