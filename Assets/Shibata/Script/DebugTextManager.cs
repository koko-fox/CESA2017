using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTextManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("デバッグ描画用テキストエリア")]
	private UnityEngine.UI.Text textArea;

	// Use this for initialization
	void Start ()
	{
		DebugTextWriter.SetTextArea(textArea);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		DebugTextWriter.ReflectionText();
	}
}