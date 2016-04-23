using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DoorControl))]
public class DoorEditor : Editor{
	public override void OnInspectorGUI ()
	{
		DoorControl door = (DoorControl)target;

		DrawDefaultInspector ();

		EditorGUILayout.HelpBox (
			"The red cuboid shows the closed door position and the green cuboid shows the open position", 
			MessageType.Info
		);

		if (GUILayout.Button("Move to start position")) {
			if (door.OpenOnStart) {
				door.transform.position = door.OpenedPosition.position;
			} else {
				door.transform.position = door.ClosedPosition.position;
			}
		}
	}
}