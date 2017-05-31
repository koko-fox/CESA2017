using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[BrowsableAttribute(false), EditorBrowsable(EditorBrowsableState.Never)]
public class EditorHidedMonoBehaviour : MonoBehaviour
{
	private new string ToString() { return ""; }
}
