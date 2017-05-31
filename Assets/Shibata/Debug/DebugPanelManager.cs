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

	private void FixedUpdate()
	{
		if (!_debugCanvas)
			return;

		List<DebugPanel> list = new List<DebugPanel>();
		foreach (Transform elem in _debugCanvas.transform)
		{
			var panel = elem.GetComponent<DebugPanel>();
			if (panel)
				list.Add(panel);
		}

		list.Sort((DebugPanel lhs, DebugPanel rhs) =>
		{
		float ldist = Vector3.Distance(Camera.main.transform.position, lhs._owner.transform.position);
			float rdist = Vector3.Distance(Camera.main.transform.position, rhs._owner.transform.position);
			return ldist - rdist > 0 ? -1 : 1;
		});

		for (int f1 = 0; f1 < list.Count; f1++)
			list[f1].transform.SetAsLastSibling();
	}

	public DebugPanel Create(GameObject owner)
	{
		var obj = Instantiate(_original);
		var panel = obj.GetComponent<DebugPanel>();
		panel._owner = owner;

		return panel;
	}
}
