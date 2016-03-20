using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GroundGridBorderPresenter : MonoBehaviour 
{
	[SerializeField]
	private Image _image;

	[SerializeField]
	private Image[] _cornerImages;

	[SerializeField]
	private BorderStretchType _stretchType;
	public BorderStretchType StretchType { get { return _stretchType; } }

	public void SetVisibility(bool value)
	{
		_image.enabled = value;

		if (_cornerImages != null)
			for (int i = 0; i < _cornerImages.Length; i++)
				_cornerImages[i].enabled = value;
	}
}

public enum BorderStretchType
{
	Width,
	Height
}
