using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Utilities;

namespace GameCanvas
{
	public class GroundGridPresenter : MonoBehaviour 
	{
		[SerializeField]
		private Vector2 _gridCellSize = new Vector2(100f,100f);

		[SerializeField]
		private int _maxGridCellCount = 2000000;

		[SerializeField]
		private Image _groundGridImage;

		[SerializeField]
		private RectTransform _groundGridRectTransform;

		private int _gridSizeX = 1;
		private int _gridSizeY = 1;

		public bool IsCanSetGridSize(Vector2 size)
		{
			int x,y;
			size.ToInt(out x, out y);

			if (x > 0 && y > 0 && x*y <= _maxGridCellCount)
			{
				return true;
			}

			return false;
		}

		public void SetGridSize(Vector2 size)
		{
			if (IsCanSetGridSize(size) == false)
				return;

			size.ToInt(out _gridSizeX, out _gridSizeY);

			float sizeDeltaX = _gridSizeX * _gridCellSize.x;
			float sizeDeltaY = _gridSizeY * _gridCellSize.y;

			_groundGridRectTransform.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);
		}

		public Vector2 GetGroundRealSize()
		{
			float sizeDeltaX = _gridSizeX * _gridCellSize.x;
			float sizeDeltaY = _gridSizeY * _gridCellSize.y;

			return new Vector2(sizeDeltaX, sizeDeltaY);
		}
	}
}


