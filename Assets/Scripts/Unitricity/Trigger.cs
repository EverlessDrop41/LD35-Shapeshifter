using UnityEngine;
using System.Collections;

public class Trigger: ElectricalObject
{
    public ElectricalObject Input;

    public override bool IsOutputting()
    {
        return _hasBeenTurnedOn;
    }

    private bool _hasBeenTurnedOn = false;

    void Update () {
        if (!_hasBeenTurnedOn)
        {
            if (Input.IsOutputting())
            {
                _hasBeenTurnedOn = true;
            }
        }
	}
}
