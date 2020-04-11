using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public GameObject hand;

    public void pickUp()
    {
        hand.SetActive(false);
    }

    public void drop()
    {
        hand.SetActive(true);
    }
}
