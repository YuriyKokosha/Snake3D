using UnityEngine;
using System.Collections;

namespace EventsSystem
{
	public class EventCameraMoved : EventBase 
	{
		public Vector3 Position { get; private set; }

		public EventCameraMoved(Vector3 position)
		{
			this.Position = position;
		}
	}

	public class EventFPSValueChanged : EventBase 
	{
		public float Value { get; private set; }

		public EventFPSValueChanged(float value)
		{
			this.Value = value;
		}
	}

	public class EventSpeedChanged : EventBase 
	{
		public float Value { get; private set; }

		public EventSpeedChanged(float value)
		{
			this.Value = value;
		}
	}
}
