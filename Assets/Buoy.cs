using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Buoy : MonoBehaviour
{

    private Animator ani;
    public float maxDelay = 3f;
    public bool canSpawnSubs = false;

    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        StartCoroutine(startAnimation(Random.Range(0f, maxDelay)));
    }

    IEnumerator startAnimation(float delay)
    {   

        yield return new WaitForSeconds(delay);

        //buoys that can spawn subs under them will have red lights
        if (canSpawnSubs)
        {
            ani.SetTrigger("StartRed");
        }
        else
        {
            ani.SetTrigger("Start");
        }


        
    }
}
