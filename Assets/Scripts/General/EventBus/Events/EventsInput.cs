using UnityEngine;
using System.Collections;

namespace EventsSystem
{
	public class EventOnButtonClicked : EventBase 
	{
		public UIButtonType Type { get; private set; }

		public EventOnButtonClicked(UIButtonType type)
		{
			this.Type = type;
		}
	}
}
