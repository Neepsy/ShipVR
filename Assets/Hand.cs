using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{

    public GameObject hand;
    public LineRenderer line;
    public XRRayInteractor ray;
    public float lineWidth = .02f;

    private bool isHoldingObject;
    private bool lineEnabled;
    //layer 5 (0 index) is ui, 9 is pickup, 11 is depth charge
    private int bitmask = 0b101000100000;

    private void Update()
    {

    }

    public void pickUp()
    {
        hand.SetActive(false);
    }

    public void drop()
    {
        hand.SetActive(true);
    }

    private void toggleRay(bool b)
    {
        lineEnabled = b;
        if (b)
        {
            line.endWidth = lineWidth;
            line.startWidth = lineWidth;
        }
        else
        {
            line.endWidth = 0;
            line.startWidth = 0;
        }
        Debug.Log(b);
    }
}
