using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugBoardRegister
{
	#region inner class
	public class Panel
	{
		int _id;
		public int id { get { return _id; } }
		string _title;
		public string title
		{
			get { return _title; }
			set { _title = value; }
		}
		string _text;
		public string text
		{
			get { return _text; }
			set { _text = value; }
		}
		Rect _rect;
		public Rect rect { get { return _rect; } }

		public Panel(int id, Rect rect)
		{
			_id = id;
			_rect = rect;
		}
	}
	#endregion

	#region private fields
	static Rect _sourceRect = new Rect(0, 0, 200, 200);
	static float _mergin = 10.0f;
	static int _idCount = 0;
	static List<Panel> _panels = new List<Panel>();
	#endregion

	#region properties
	/// <summary>登録されているパネル情報</summary>
	public static List<Panel> panels
	{
		get { return _panels; }
	}
	#endregion

	#region public methods
	/// <summary>パネル情報を取得する</summary>
	/// <param name="id">パネルID</param>
	/// <returns>見つかったパネル</returns>
	public static Panel GetPanel(int id)
	{
		return _panels.Find(panel => panel.id == id);
	}

	/// <summary>パネルを作成する</summary>
	/// <returns>作成されたパネル</returns>
	public static Panel CreatePanel()
	{
		var rect = _sourceRect;
		rect.x = _idCount * _sourceRect.width + _idCount * _mergin;
		var panel = new Panel(_idCount++, rect);
		_panels.Add(panel);
		return panel;
	}
	#endregion
}
