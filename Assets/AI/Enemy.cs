using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Target))]
public class Enemy : MonoBehaviour
{
    [Tooltip("Minimum distance the enemy is try to stay from the player")]
    public float minDis;
    [Tooltip("Maximum distance the enemy is try to stay from the player")]
    public float maxDis;

    [Tooltip("Maximum distance that this enemy can move per frame (due to the state machine running each frame")]
    public float moveDis;


    //do attack actions here, such as raycast for a laser, spawn in a missile, etc.
    //protected abstract void Attack();

    private void Selfdestruct()
    {
        GetComponent<Target>().damage(99999);
    }
}
