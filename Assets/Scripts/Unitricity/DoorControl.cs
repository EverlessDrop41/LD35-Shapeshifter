using UnityEngine;
using System.Collections;

[AddComponentMenu("Electricity/Door")]
public class DoorControl: MonoBehaviour {
	public Transform OpenedPosition;
	public Transform ClosedPosition;

	public ElectricalObject input;

	public float MoveSpeed = 1f;

	public bool OpenOnStart = true;

	public bool isOpen {
		get;
		private set;
	}

	bool isClosing, isOpening;

	bool isTransitioning {
		get {
			return isClosing || isOpening;
		}
	}

	void Start() {
		#region Check Editor Inputs
		if (OpenedPosition) {
			if (!ClosedPosition) {
				Debug.LogErrorFormat("[{0}] Door component has no Closed Position", gameObject.name);
			}
		} else if (ClosedPosition) {
			Debug.LogErrorFormat("[{0}] Door component has no Opened Position", gameObject.name);
		} else {
			Debug.LogErrorFormat("[{0}] Door component has no Opened or Closed Positions", gameObject.name);
		}

		if (!input) {
			Debug.LogErrorFormat("[{0}] Door component has no Input Object", gameObject.name);
		}
		#endregion

		Vector3 openPos = (OpenOnStart ? OpenedPosition.position : ClosedPosition.position);
		transform.position = openPos;
	}

	void Update() {
		if (input.IsOutputting()) {
			Open();
		} else {
			Close();
		}

		if (isOpening) {
			if (!Utils.VectorRoughlyEqual(transform.position, OpenedPosition.position, 0.5f)) {
				transform.position = Vector3.MoveTowards(transform.position, OpenedPosition.position, MoveSpeed * Time.deltaTime);
			} else {
				isOpening = false;
			}
		} else if (isClosing) {
			if (!Utils.VectorRoughlyEqual(transform.position, ClosedPosition.position, 0.5f)) {
				transform.position = Vector3.MoveTowards(transform.position, ClosedPosition.position, MoveSpeed * Time.deltaTime);
			} else {
				isClosing = false;
			}
		}
	}

	public void ToggleOpenState() {
		if (!isTransitioning) {
			if (isOpen) {
				Close();
			} else {
				Open();
			}
		}
	}

	public void Open() {
		isOpening = true;
		isClosing = false;
	}

	public void Close() {
		isClosing = true;
		isOpening = false;
	}

	public void OnDrawGizmos() {
		Color closedDoorColor = new Color (255, 0, 0, .5f);
		Color openDoorColor = new Color (0, 255, 0, .5f);
		if (OpenedPosition && ClosedPosition) {
			Gizmos.color = Color.green;
			Gizmos.DrawLine (transform.position, OpenedPosition.position);
			Gizmos.color = openDoorColor;
			Gizmos.DrawCube (OpenedPosition.position, transform.lossyScale);

			Gizmos.color = Color.red;
			Gizmos.DrawLine (transform.position, ClosedPosition.position);
			Gizmos.color = closedDoorColor;
			Gizmos.DrawCube (ClosedPosition.position, transform.lossyScale);
		}

		if (input) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (transform.position, input.transform.position);
		}
	}
}