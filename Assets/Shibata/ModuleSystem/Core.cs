using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
	protected List<Module> _modules = new List<Module>();
	public List<Module> modules { get { return _modules; } }

	public void ApplyMod(Module mod)
	{
		_modules.Add(mod);
		_modules.Sort((Module lhs, Module rhs) => lhs.priority - rhs.priority);
	}

	protected virtual void Start()
	{
		foreach (var elem in _modules)
			elem.OrdableStart();
	}

	protected virtual void Update()
	{
		foreach (var elem in _modules)
		{
			if (!elem.locked)
				elem.OrdableUpdate();
		}
	}

	protected virtual void FixedUpdate()
	{
		foreach (var elem in _modules)
		{
			if (!elem.locked)
				elem.OrdableFixedUpdate();
		}
	}
}
