using UnityEngine;
using System.Collections;
using EventsSystem;

public class GameSceneController 
	: MonoBehaviour 
	, IHandleEvent<EventOnButtonClicked>

{
	void OnEnable()
	{
		EventBus.Instance.Subscribe<EventOnButtonClicked>(this);
	}

	void OnDisable()
	{
		if (EventBus.Exists)
			EventBus.Instance.Unsubscribe<EventOnButtonClicked>(this);
	}


	#region IHandleEvent implementation

	public void Handle(object sender, EventOnButtonClicked data)
	{
		if (data.Type == UIButtonType.Quit)
		{
			Application.Quit();
		}
	}

	#endregion
}
