using UnityEngine;
using System.Collections;

/*
 This object is here to combine MonoBehaviour and IElectricalItem
*/

public abstract class ElectricalObject : MonoBehaviour, IElectricalItem
{
    public abstract bool IsOutputting();
}
