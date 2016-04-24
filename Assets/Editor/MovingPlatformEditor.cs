using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor {

	public override void OnInspectorGUI ()
	{
		MovingPlatform mp = target as MovingPlatform;
		DrawDefaultInspector ();

		float dist = Vector3.Distance (mp.PositionB, mp.PositionA);

		EditorGUILayout.HelpBox (
			string.Format("The platforms are {0} units apart\n\nIt will take {1} seconds for the platform to move to each point", dist,dist/mp.MoveSpeed),
			MessageType.None
		);

		EditorGUILayout.HelpBox (
			"Green: Point A, Red: Point B",
			MessageType.Info
		);

		GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
				GUILayout.Label ("Move Platform");
			GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		if (GUILayout.Button("Move to PositionA")) {
			mp.transform.position = mp.PositionA;
		}

		if (GUILayout.Button("Move to PositionB")) {
			mp.transform.position = mp.PositionB;
		}

		if (GUILayout.Button("Move to Center")) {
			mp.transform.position = (mp.PositionA + mp.PositionB) / 2;
		}
	}

	public void OnSceneGUI () {
		MovingPlatform mp = target as MovingPlatform;
	
		Handles.color = Color.white;
		Handles.DrawDottedLine (mp.PositionA, mp.PositionB, 3);
		Handles.color = Color.green;
		mp.PositionA = Handles.FreeMoveHandle (mp.PositionA, Quaternion.identity, .1f, new Vector3(.5f, .5f, .5f), Handles.DotCap);
		Handles.color = Color.red;
		mp.PositionB = Handles.FreeMoveHandle (mp.PositionB, Quaternion.identity, .1f, new Vector3(.5f, .5f, .5f), Handles.DotCap);
	}
}