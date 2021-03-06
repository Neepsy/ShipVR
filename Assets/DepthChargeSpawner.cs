﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthChargeSpawner : MonoBehaviour
{

    public int respawnTime = 300;
    public GameObject depthCharge;
    private int timeLeft = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > -1)
        {
            if (timeLeft == 0)
            {
                GameObject charge = Instantiate(depthCharge, transform.position, Quaternion.identity);
                charge.GetComponent<DepthCharge>().spawner = this;
                Debug.Log("spawning");
                charge.transform.parent = gameObject.transform;
                charge.transform.rotation = gameObject.transform.rotation;
            }
            timeLeft--;
        }
    }

    public void beginCountdown()
    {
        timeLeft = respawnTime;
    }
}
