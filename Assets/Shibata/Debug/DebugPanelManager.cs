using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanelManager : MonoBehaviour
{
	static DebugPanelManager _instance;
	public static DebugPanelManager instance
	{
		get
		{
			if(!_instance)
			{
				var obj = new GameObject("DebugPanelManager");
				_instance = obj.AddComponent<DebugPanelManager>();
				_instance._debugCanvas = GameObject.Find("DebugCanvas").GetComponent<Canvas>();
				_instance._original = (GameObject)Resources.Load("DebugPanel");
			}
			return _instance;
		}
	}

	[SerializeField]
	Canvas _debugCanvas;
	[SerializeField]
	GameObject _original; 

	private void Awake()
	{
	}

	public DebugPanel Create(GameObject owner)
	{
		var obj = Instantiate(_original);
		var panel = obj.GetComponent<DebugPanel>();
		panel._owner = owner;

		return panel;
	}
}
