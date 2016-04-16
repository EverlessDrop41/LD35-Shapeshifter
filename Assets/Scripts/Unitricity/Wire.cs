using UnityEngine;
using System.Collections;

[AddComponentMenu("Electricity/Wire")]
[RequireComponent(typeof(Renderer))]
public class Wire: ElectricalObject {
	public ElectricalObject Input;

	Renderer R;

	public Color OnColour, OffColour;

	public override bool IsOutputting() {
		return Input.IsOutputting();
	}

	public void Start() {
		R = GetComponent <Renderer> ();
	}

	void Update() {
		R.material.color = (Input.IsOutputting() ? OnColour : OffColour);
	}
}