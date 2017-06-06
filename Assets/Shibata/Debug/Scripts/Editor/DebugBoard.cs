using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugBoard : EditorWindow
{
	[MenuItem("Window/Debug Board")]
	static void ShowEditor()
	{
		DebugBoard board = GetWindow<DebugBoard>();
		board.Show();
	}

	void OnGUI()
	{
		BeginWindows();

		foreach(var elem in DebugBoardRegister.panels)
		{
			GUI.Window(elem.id, elem.rect, DrawWindow, elem.title);
		}
		EndWindows();
	}

	void DrawWindow(int id)
	{
		GUI.DragWindow();
		GUIStyle style = new GUIStyle();
		style.richText = true;
		style.wordWrap = true;

		var panel = DebugBoardRegister.GetPanel(id);

		var rect = panel.rect;
		rect.x = 5;
		rect.y = 15;

		GUI.Label(rect, panel.text, style);
	}

	void Update()
	{
		Repaint();
	}
}
