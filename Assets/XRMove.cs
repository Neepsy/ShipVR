using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XRMove : MonoBehaviour
{

    CharacterController controller;
    Movement input;
    Vector2 rawVec;
    public float moveSpeedMod = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rawVec.x != 0 || rawVec.y != 0)
        {
            rawVec *= moveSpeedMod;
            Vector3 moveVec = new Vector3(rawVec.x, 0f, rawVec.y);
            moveVec = Camera.main.transform.TransformDirection(moveVec);
            moveVec.y = 0.0f;
            controller.Move(moveVec);
        }
        
    }

    private void Awake()
    {
        input = new Movement();
        input.Player.Move.performed += ctx => rawVec = ctx.ReadValue<Vector2>();
    }

    public void recievedInput()
    {
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
