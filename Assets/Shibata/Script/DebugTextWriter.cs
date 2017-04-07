using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デバッグテキスト描画用クラス
/// </summary>
public static class DebugTextWriter
{
	//デバッグ用テキスト領域
	private static UnityEngine.UI.Text textArea;

	//テキストバッファ
	private static Queue<string> textBuffer = new Queue<string>();

	/// <summary>
	/// テキストエリアを設定する
	/// </summary>
	/// <param name="textArea">テキストエリア</param>
	public static void SetTextArea(UnityEngine.UI.Text textArea)
	{
		DebugTextWriter.textArea = textArea;
	}

	/// <summary>
	/// テキストを書き込む
	/// </summary>
	/// <param name="text">テキスト</param>
	public static void Write(string text)
	{
		textBuffer.Enqueue(text);
	}

	/// <summary>
	/// テキストバッファをクリアする
	/// </summary>
	public static void ClearTextBuffer()
	{
		textBuffer.Clear();
	}
	
	/// <summary>
	/// テキストの変更を反映する
	/// </summary>
	public static void ReflectionText()
	{
		textArea.text = "";
		while(textBuffer.Count!=0)
		{
			string str=textBuffer.Dequeue();
			textArea.text += str;
			textArea.text += "\n";
		}
	}
}
