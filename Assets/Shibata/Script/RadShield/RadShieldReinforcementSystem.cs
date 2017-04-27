using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RadShieldReinforcementSystem : MonoBehaviour
{
	[SerializeField]
	private float[] _chargeTimeByLevel;

	[SerializeField]
	private Material[] _materialByLevel;

	[SerializeField]
	private float[] _powerMultiplierByLevel;

	[SerializeField]
	private float[] _scaleByLevel;

	[SerializeField]
	private float[] _lifeTimeMultiplierByLevel;

	private RadShieldCore _coreSystem;
	private RadShieldMovementSystem _movementSystem;
	private MeshRenderer _meshRenderer;

	private int _level = 0;
	private int _maxLevel;
	private float _chargeElapsedTime = 0.0f;
	private Vector3 _baseScale;

	private void Awake()
	{
		_coreSystem = GetComponent<RadShieldCore>();
		_movementSystem = GetComponent<RadShieldMovementSystem>();
		_meshRenderer = GetComponent<MeshRenderer>();
		_meshRenderer.material = _materialByLevel[_level];
		_maxLevel = _chargeTimeByLevel.Length - 1;
		_baseScale = transform.localScale;

		_coreSystem.OnCalcAttackPower += () => { return _powerMultiplierByLevel[_level]; };
	}

	private void FixedUpdate()
	{
		DebugTextWriter.Write("level:" + _level.ToString() + "/" + _maxLevel);
	}

	private void Update()
	{
		if (!_movementSystem.MoveEnabled && _level < _maxLevel)
		{
			_chargeElapsedTime += Time.deltaTime;
			if(_chargeElapsedTime>=_chargeTimeByLevel[_level])
			{
				_level++;
				_meshRenderer.material = _materialByLevel[_level];

				Vector3 scale = new Vector3(_scaleByLevel[_level] * _baseScale.x, _baseScale.y, _baseScale.z);

				transform.DOScale(scale, 0.1f);

				_chargeElapsedTime = 0.0f;
			}
		}
	}
}
