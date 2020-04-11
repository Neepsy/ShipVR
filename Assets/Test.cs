using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    Movement input;
    Vector2 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        input = new Movement();
        input.Player.Move.performed += ctx => moveVec = ctx.ReadValue<Vector2>();
    }


    public void recievedInput()
    {
        Debug.Log(moveVec);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void onDisable()
    {
        input.Disable();
    }
}
