using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsSystem
{
	public class EventBus : MonoSingleton<EventBus>
	{
		private Dictionary<Type, List<WeakReference>> _allHandlers;

		public override void Init()
		{
			_allHandlers = new Dictionary<Type, List<WeakReference>> ();
		}

		public void Subscribe<T>(IHandleEvent<T> handler)
		{
			List<WeakReference> handlers;

			lock (_allHandlers)
			{
				if(_allHandlers.TryGetValue(typeof(T), out handlers) == false)
				{
					handlers = new List<WeakReference>();
					_allHandlers.Add(typeof(T), handlers);
				}

				handlers.Add(new WeakReference(handler));
			}
		}

		public void Unsubscribe<T>(IHandleEvent<T> handler)
		{
			List<WeakReference> handlers;

			lock (_allHandlers)
			{
				if(_allHandlers.TryGetValue(typeof(T), out handlers) == true)
				{
					handlers.RemoveAll(wr => ReferenceEquals(wr.Target, handler));
				}
			}
		}

		public void Post<T>(object sender, T data)
		{
			List<WeakReference> handlers;

			lock (_allHandlers)
			{
				if(_allHandlers.TryGetValue(typeof(T), out handlers) == true)
				{
					var handlersList = handlers.ToList();
					for(int i = 0; i < handlersList.Count; i++)
					{
						var wr = handlersList[i];
						if (wr.IsAlive)
						{
							var handler = handlersList[i].Target as IHandleEvent<T>;
							if(handler != null)
							{
								handler.Handle(sender, data);
							}
							else
							{
								handlers.Remove(wr);
							}
						}
						else
						{
							handlers.Remove(wr);
						}
					}
				}
			}
		}
	}
}
