using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterConstructorModule : MonoBehaviour
{
	[SerializeField]
	private GameObject[] _items;

	private int _selectedIndex = 0;

	private Camera _camera;

	private void Awake()
	{
		_camera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			_selectedIndex = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2))
			_selectedIndex = 1;
		if (Input.GetKeyDown(KeyCode.Alpha3))
			_selectedIndex = 2;
		if (Input.GetKeyDown(KeyCode.Alpha4))
			_selectedIndex = 3;
		if (Input.GetKeyDown(KeyCode.Alpha5))
			_selectedIndex = 4;

		Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
		Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

		if(Input.GetKeyDown(KeyCode.F))
		{
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				var obj = Instantiate(_items[_selectedIndex]);
				obj.transform.position = hit.point;
			}
		}
	}
}
