using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject shell;

    [Tooltip("Make sure this also has the sound and particle effect attached")]
    public GameObject shootLocation;
    public float shootForce = 20.0f;
    public float reloadTime = 2.0f;

    private float reload = 0.0f;
    ParticleSystem ps;
    AudioSource aud;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = shootLocation.GetComponent<ParticleSystem>();
        aud = shootLocation.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(reload > 0f)
        {
            reload -= Time.deltaTime;
        }
    }

    public void fire()
    {
        if(reload <= 0.0f)
        {
            GameObject projectile = Instantiate(shell, shootLocation.transform.position, Quaternion.identity);
            projectile.transform.rotation = transform.rotation;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(projectile.transform.forward * -shootForce);

            Destroy(projectile, 20f);
            reload = reloadTime;

            //do effects if not null
            if(ps != null)
            {
                ps.Play(true);
            }

            if(aud != null)
            {
                aud.Play();
            }
        }
        
    }

}
