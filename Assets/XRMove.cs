using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XRMove : MonoBehaviour
{

    CharacterController controller;
    Movement input;
    Vector2 rawVec;
    float timeMoving;
    //Vector3 lastPos, velocity;
    [Header("Movement Properties")]
    public float moveSpeedMod = 0.05f;
    public float maxMoveSpeedMod = 0.07f;
    float moveSpeedRatio;
    public float accelTime = 3f;

    [Header("Wake Properties")]
    public ParticleSystem wake;
    ParticleSystem.EmissionModule emission;
    public Transform wakePivot;
    public float wakeEmissionMult = 300;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        emission = wake.emission;
        moveSpeedRatio = maxMoveSpeedMod / moveSpeedMod;
    }

    // Update is called once per frame
    void Update()
    {
        if(rawVec.x != 0 || rawVec.y != 0)
        {
            timeMoving += Time.deltaTime;

            rawVec *= moveSpeedMod;
            Vector3 moveVec = new Vector3(rawVec.x, 0f, rawVec.y);
            moveVec = Camera.main.transform.TransformDirection(moveVec);
            moveVec.y = 0.0f;

            //handles acceleration
            moveVec *= System.Math.Max(1f, moveSpeedRatio * System.Math.Min(1f, (timeMoving / accelTime)));

            controller.Move(moveVec);

            if(moveVec.magnitude > .01)
            {
                wakePivot.transform.rotation = Quaternion.LookRotation((-30 * moveVec), Vector3.up);
            }

            emission.rateOverTime = (moveVec.magnitude) * 60 * wakeEmissionMult;
        }
        else
        {
            //not moving, turn off wake
            emission.rateOverTime = 0;
            timeMoving = 0;
        }

        
;    }

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
