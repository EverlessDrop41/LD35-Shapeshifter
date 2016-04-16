using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Electricity/Source")]
public class Source : ElectricalObject {

    public override bool IsOutputting() {
        return true;
    }
}
