using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //The gameobeject this state will control
    protected GameObject gameObject;

    //Object that represents the player, probably the main camera
    protected GameObject player;

    //The Enemy script attached to the gameObject;
    protected Enemy enemy;

    public abstract Type Execute();
    public State(GameObject thisObj, GameObject player)
    {
        gameObject = thisObj;
        this.player = player;
        enemy = thisObj.GetComponent<Enemy>();
    }
}

