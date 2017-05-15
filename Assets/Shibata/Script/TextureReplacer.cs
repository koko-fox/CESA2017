using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureReplacer : MonoBehaviour
{
	[SerializeField]
	Material _material;

	[SerializeField]
	Texture2D _texture;

	private void Start()
	{
		var image = GetComponent<Image>();
		var renderer = GetComponent<CanvasRenderer>();

		renderer.SetMaterial(_material, _texture);
	}
}
