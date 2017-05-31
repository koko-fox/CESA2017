using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
	#region properties
	bool _locked = false;
	/// <summary>
	/// モジュールをロックするかどうかのフラグ
	/// </summary>
	public bool locked
	{
		get { return _locked; }
		set { _locked = value; }
	}

	int _priority;
	/// <summary>
	/// 処理優先順位
	/// </summary>
	public int priority
	{
		get { return _priority; }
		protected set { _priority = value; }
	}
	#endregion

	void Awake()
	{
		var core = GetComponent<Core>();
		core.ApplyMod(this);
		ModuleAwake();
	}

	protected virtual void ModuleAwake() { }

	#region ordable methods
	public virtual void OrdableStart() { }
	public virtual void OrdableUpdate() { }
	public virtual void OrdableFixedUpdate() { }
	#endregion
}
