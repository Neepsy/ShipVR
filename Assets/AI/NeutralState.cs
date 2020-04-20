using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralState : State
{

    //Default state. Does not move



    public NeutralState(GameObject thisObj, GameObject player)
           : base(thisObj, player) { }
  
    public override Type Execute()
    {

        //bob up and down in place
        Vector3 pos = gameObject.transform.position;
        pos.y += (float) (enemy.moveDis * Time.deltaTime * Math.Sin(Time.time));
        gameObject.transform.position = pos;


        //too low, gain altitude
        if(pos.y <= 1)
        {
            return typeof(AscendState);
        }

        float dis = Vector3.Distance(pos, player.transform.position);
        if(dis > enemy.maxDis)
        {
            //return typeof(ApproachState)
        }
        else if(dis < enemy.minDis)
        {
            //return typeof(RetreatState)
        }

        return typeof(NeutralState);
    
    }
}
