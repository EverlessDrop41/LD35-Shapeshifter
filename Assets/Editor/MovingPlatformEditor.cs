using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor {

	public override void OnInspectorGUI ()
	{
		MovingPlatform mp = target as MovingPlatform;
		DrawDefaultInspector ();

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
	
		mp.PositionA = Handles.FreeMoveHandle (mp.PositionA, Quaternion.identity, .1f, new Vector3(.5f, .5f, .5f), Handles.DotCap);
		mp.PositionB = Handles.FreeMoveHandle (mp.PositionB, Quaternion.identity, .1f, new Vector3(.5f, .5f, .5f), Handles.DotCap);
		Handles.DrawLine (mp.PositionA, mp.PositionB);
	}
}