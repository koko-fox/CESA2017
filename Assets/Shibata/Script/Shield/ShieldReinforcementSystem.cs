using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldReinforcementSystem : Lockable
{
	const int _maxLevel = 3;
	int _level = 0;

	ShieldCore _core;
	
	[SerializeField]
	float[] _chargeTimes = new float[_maxLevel];
	[SerializeField]
	float[] _attackMultipliers = new float[_maxLevel];
	[SerializeField]
	float[] _scales = new float[_maxLevel];
	[SerializeField]
	float[] _lifeTimeMultipliers = new float[_maxLevel];

	Vector3 _baseScale;
	float _chargeElapsedTime = 0.0f;

	private void Awake()
	{
		_core = GetComponent<ShieldCore>();

		_core.attackSystem.onCalcValue += () => { return _attackMultipliers[_level]; };

		_baseScale = transform.localScale;
	}

	private void Start()
	{
		transform.localScale = new Vector3(0.0f, _baseScale.y, _baseScale.z);
		transform.DOScaleX(_baseScale.x, 0.2f);
	}

	protected override void LockableUpdate()
	{
		if (_core.destroySystem.isLock && _level < _maxLevel)
		{
			_chargeElapsedTime += Time.deltaTime;
			if(_chargeElapsedTime>=_chargeTimes[_level])
			{
				Vector3 scale = new Vector3(_scales[_level] * _baseScale.x, _baseScale.y, _baseScale.z);
				transform.DOScale(scale, 0.2f);

				_chargeElapsedTime = 0.0f;
				_level++;
			}
		}
	}
}
