using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TFDebug
{
	#region structs
	public struct LogData
	{
		/// <summary>本文</summary>
		public string text;
		/// <summary>タイムスタンプ</summary>
		public float timeStamp;

		public LogData(string text,float timeStamp)
		{
			this.text = text;
			this.timeStamp = timeStamp;
		}
	}
	#endregion

	#region private fields
	static Dictionary<string,Queue<LogData>> _columns = new Dictionary<string, Queue<LogData>>();
	static int _capacity = 32;

	static Dictionary<string,string> _monitors=new Dictionary<string, string>();

	#endregion

	#region properties
	/// <summary>カラムのデータ</summary>
	public static Dictionary<string,Queue<LogData>> columns
	{
		get { return _columns; }
	}

	/// <summary>モニタのデータ</summary>
	public static Dictionary<string,string> monitors
	{
		get { return _monitors; }
	}
	#endregion

	#region public methods
	/// <summary>デバッグログを追加する</summary>
	/// <param name="columnName">カラム名</param>
	/// <param name="text">本文</param>
	public static void Log(string columnName,string text)
	{
		//キーが存在しない場合は新規にカラムを作成
		if(!_columns.ContainsKey(columnName))
		{
			_columns.Add(columnName, new Queue<LogData>());
		}

		LogData logData = new LogData(string.Format("<color=#c0c0c0>{0}</color>",text),Time.time);
		_columns[columnName].Enqueue(logData);
		if (_columns[columnName].Count > _capacity)
		{
			_columns[columnName].Dequeue();
		}
	}

	/// <summary>指定カラムのログを消去する</summary>
	/// <param name="columnName">カラム名</param>
	/// <returns>指定カラムが存在しない場合はfalseを返す</returns>
	public static bool ClearLogColumn(string columnName)
	{
		if(!_columns.ContainsKey(columnName))
		{
			return false;
		}

		_columns[columnName].Clear();
		return true;
	}

	/// <summary>指定カラムを除去する</summary>
	/// <param name="columnName">カラム名</param>
	/// <returns>指定カラムが存在しない場合はfalseを返す</returns>
	public static bool RemoveLogColumn(string columnName)
	{
		if(!_columns.ContainsKey(columnName))
		{
			return false;
		}

		_columns.Remove(columnName);
		return true;
	}

	/// <summary>デバッグモニタに書き込む</summary>
	/// <param name="monitorName">モニタ名</param>
	/// <param name="text">テキスト</param>
	public static void Write(string monitorName, string text)
	{
		if(!_monitors.ContainsKey(monitorName))
		{
			_monitors.Add(monitorName, "");
		}

		_monitors[monitorName] += string.Format("<color=#c0c0c0>{0}</color>",text);
	}

	/// <summary>指定デバッグモニタをクリアする</summary>
	/// <param name="monitorName">モニタ名</param>
	/// <returns>指定モニタが存在しない場合はfalse</returns>
	public static bool ClearMonitor(string monitorName)
	{
		if(!_monitors.ContainsKey(monitorName))
		{
			return false;
		}

		_monitors[monitorName] = "";
		return true;
	}

	/// <summary>指定モニタを削除する</summary>
	/// <param name="monitorName">モニタ名</param>
	/// <returns>指定モニタが存在しない場合はfalse</returns>
	public static bool RemoveMonitor(string monitorName)
	{
		if(!_monitors.ContainsKey(monitorName))
		{
			return false;
		}

		_monitors.Remove(monitorName);
		return true;
	}
	#endregion
}
