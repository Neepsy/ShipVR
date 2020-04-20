using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendState : State
{
    public AscendState(GameObject thisObj, GameObject player)
           : base(thisObj, player) { }

    //state executed when this enemy is too close to the surface of the water

    public override Type Execute()
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += enemy.moveDis;
        gameObject.transform.position = pos;

        if(pos.y <= 1)
        {
            return typeof(AscendState);
        }

        return typeof(NeutralState);
         
    }
}
