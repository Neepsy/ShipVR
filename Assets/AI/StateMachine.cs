using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(Enemy))]
public class StateMachine : MonoBehaviour
{

    private State currentState;
    Dictionary<Type, State> avaliableStates;
    public GameObject player;

    void Awake()
    {
        if(player == null)
        {
            player = Camera.main.gameObject;
        }
        Dictionary<Type, State> dic = new Dictionary<Type, State>();
        dic.Add(typeof(NeutralState), new NeutralState(gameObject, player));
        dic.Add(typeof(AscendState), new AscendState(gameObject, player));
        
        Initialize(dic);
    }

    public void Initialize(Dictionary<Type, State> dic)
    {
        avaliableStates = dic;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == null)
        {
            currentState = avaliableStates.Values.First();
        }

        Type next = currentState.Execute();
        if(next != currentState.GetType())
        {
            currentState = avaliableStates[next];
            Debug.Log("Switching state: " + next);
        }
        else
        {
            Debug.Log("Maintaining state: " + next);
        }
    }
}
