using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TFColor
{
	#region properties

	public static class Monotone
	{
		public static Color black
		{
			get { return GetColorFromCode(0x000000); }
		}

		public static Color dimgray
		{
			get { return GetColorFromCode(0x696969); }
		}

		public static Color gray
		{
			get { return GetColorFromCode(0x808080); }
		}

		public static Color darkgray
		{
			get { return GetColorFromCode(0xa9a9a9); }
		}

		public static Color silver
		{
			get { return GetColorFromCode(0xc0c0c0); }
		}

		public static Color lightgrey
		{
			get { return GetColorFromCode(0xd3d3d3); }
		}

		public static Color gainsboro
		{
			get { return GetColorFromCode(0xdcdcdc); }
		}

		public static Color whitesmoke
		{
			get { return GetColorFromCode(0xf5f5f5); }
		}

		public static Color white
		{
			get { return GetColorFromCode(0xffffff); }
		}
	}

	#endregion

	#region public methods
	public static Color GetColorFromCode(int colorCode)
	{
		byte r = (byte)(colorCode&0xff0000>>(4*4));
		byte g = (byte)(colorCode&0x00ff00>>(2*4));
		byte b = (byte)(colorCode&0x0000ff);

		return new Color32(r, g, b, 0xff);
	}
	#endregion
}
