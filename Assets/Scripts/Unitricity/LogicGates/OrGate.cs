using UnityEngine;
using System.Collections;

public class OrGate : ElectricalObject {

	public ElectricalObject InputA;
	public ElectricalObject InputB;

	public bool Exclusive;
	public bool Not;

	public override bool IsOutputting() {
		bool output;

		if (Exclusive) {
			output = InputA.IsOutputting () ^ InputB.IsOutputting ();
		} else {
			output = InputA.IsOutputting () || InputB.IsOutputting ();
		}

		return Not ? !output : output;
	}
}
