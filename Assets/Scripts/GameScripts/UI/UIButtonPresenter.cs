using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIButtonPresenter : MonoBehaviour 
{
	public event Action<UIButtonType> OnButtonPointerUp;
	public event Action<UIButtonType> OnButtonPointerDown;
	public event Action<UIButtonType> OnButtonClicked;

	[SerializeField]
	private UIButtonType _buttonType;

	[SerializeField]
	private Button _button;

	public void OnPointerUp()
	{
		if (OnButtonPointerUp != null)
			OnButtonPointerUp(_buttonType);
	}

	public void OnPointerDown()
	{
		if (OnButtonPointerDown != null)
			OnButtonPointerDown(_buttonType);
	}

	public void OnClicked()
	{
		if (OnButtonClicked != null)
			OnButtonClicked(_buttonType);
	}
}

public enum UIButtonType
{
	Left = 0,
	Right = 1,
	Up = 2,
	Down = 3,
	Quit = 4
}
