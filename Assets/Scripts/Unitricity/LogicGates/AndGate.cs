using UnityEngine;
using System.Collections;

public class AndGate : ElectricalObject {

	public ElectricalObject InputA;
	public ElectricalObject InputB;

	public bool Not;

	public override bool IsOutputting() {
		bool output = InputA.IsOutputting () && InputB.IsOutputting ();

		return Not ? !output : output;
	}
}
