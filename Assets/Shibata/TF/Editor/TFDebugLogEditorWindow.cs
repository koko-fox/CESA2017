using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TFDebugLogEditorWindow : EditorWindow
{
	[MenuItem("Window/TFDebug LogWindow")]
	static void ShowEditor()
	{
		var window = GetWindow<TFDebugLogEditorWindow>();
		window.Show();
	}

	void OnGUI()
	{
		Color[] colors = {TFColor.GetColorFromCode(0x2a2a2a),TFColor.GetColorFromCode(0x303030) };

		var columns = TFDebug.columns;

		EditorGUILayout.BeginHorizontal();
		foreach(var elem in columns)
		{
			EditorGUILayout.BeginVertical();

			var width = this.position.width/columns.Count;

			var headerRect = EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(elem.Key, GUILayout.Width(width));
			EditorGUILayout.EndHorizontal();

			var logs = elem.Value.ToArray();
			for (int f2 = logs.Length - 1; f2 >= 0; f2--)
			{
				GUIStyle style = new GUIStyle();
				style.wordWrap = true;
				style.richText = true;

				var horRect = EditorGUILayout.BeginHorizontal();
				EditorGUI.DrawRect(horRect, colors[f2 % 2]);
				EditorGUILayout.LabelField(logs[f2].text, style, GUILayout.Width(width));
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndHorizontal();
	}

	void Update()
	{
		Repaint();
	}
}
