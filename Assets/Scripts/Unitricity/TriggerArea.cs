using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Electricity/TriggerArea")]
[RequireComponent(typeof(Collider2D))]
public class TriggerArea : ElectricalObject
{

    Collider2D coll;
    bool isColliding =false;

    // Use this for initialization
    void Start()
    {
        coll = GetComponent<Collider2D>();
        coll.isTrigger = true;
    }

    public override bool IsOutputting()
    {
        return isColliding;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        isColliding = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}
