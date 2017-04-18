using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterCore : MonoBehaviour
{
	private SpecterCameraControlModule _cameraControlModule;
	public SpecterCameraControlModule CameraControlModule { get { return _cameraControlModule; } }

	private SpecterMovementModule _movementModule;
	public SpecterMovementModule MovementModule { get { return _movementModule; } }

	private SpecterConstructorModule _constructorModule;
	public SpecterConstructorModule ConstructorModule { get { return _constructorModule; } }

	private void Awake()
	{
		_cameraControlModule = GetComponent<SpecterCameraControlModule>();
		_movementModule = GetComponent<SpecterMovementModule>();
		_constructorModule = GetComponent<SpecterConstructorModule>();
	}
}
