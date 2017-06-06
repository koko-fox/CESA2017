using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestWindow : EditorWindow
{
	[MenuItem("Window/TestWindow")]
	static void Open()
	{
		GetWindow<TestWindow>();
	}

	Rect node = new Rect(10f, 10f, 300f, 300f);

	private void OnGUI()
	{
		BeginWindows();
		node = GUI.Window(1, node, WindowCallback, "node");
		EndWindows();
	}

	void WindowCallback(int id)
	{
		GUI.DragWindow();

		GUIStyle style = new GUIStyle();
		style.wordWrap = true;
		style.richText = true;

		if (EditorApplication.isPlaying)
		{
		//	GUI.Label(new Rect(0f, 12f, 300f, 300f), ChanCoreSystem._buffer.ToString(), style);
		}
		else
			GUI.Label(new Rect(0, 12, 300, 300), "waiting...", style);
	}

	private void Update()
	{
		Repaint();
	}
}
