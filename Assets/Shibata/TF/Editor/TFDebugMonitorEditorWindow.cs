using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TFDebugMonitorEditorWindow : EditorWindow
{
	[MenuItem("Window/TFDebug MonitorWindow")]
	static void ShowEditor()
	{
		var window=GetWindow<TFDebugMonitorEditorWindow>();
		window.Show();
	}

	void OnGUI()
	{
		var monitors = TFDebug.monitors;
		var width = position.width/monitors.Count;

		EditorGUILayout.BeginHorizontal();

		foreach(var elem in monitors)
		{
			EditorGUILayout.BeginVertical();

			var headerRect=EditorGUILayout.BeginHorizontal();
			EditorGUI.DrawRect(headerRect, TFColor.Monotone.black);
			EditorGUILayout.LabelField(elem.Key, GUILayout.Width(width));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();

			GUIStyle style = new GUIStyle();
			style.wordWrap = true;
			style.richText = true;

			EditorGUILayout.LabelField(elem.Value,style, GUILayout.Width(width));

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndVertical();
		}

		EditorGUILayout.EndHorizontal();
	}

	void Update()
	{
		Repaint();	
	}
}
