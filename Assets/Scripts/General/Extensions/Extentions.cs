using UnityEngine;
using System;
using System.Collections.Generic;

namespace Utilities
{
	public static class VectorExtentions
	{
		public static void ToInt(this Vector2 value, out int x, out int y)
		{
			x = (int)value.x;
			y = (int)value.y;
		}
	}
}

