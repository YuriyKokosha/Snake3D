using UnityEngine;
using System.Collections;
using EventsSystem;

public class GameInputManager : MonoSingleton<GameInputManager>
{
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			OnButtonClicked(UIButtonType.Up);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			OnButtonClicked(UIButtonType.Down);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			OnButtonClicked(UIButtonType.Left);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			OnButtonClicked(UIButtonType.Right);
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnButtonClicked(UIButtonType.Quit);
		}
	}

	private void OnButtonClicked(UIButtonType type)
	{
		EventBus.Instance.Post<EventOnButtonClicked>(this, new EventOnButtonClicked(type));
	}
}
