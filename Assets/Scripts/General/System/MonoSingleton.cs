using UnityEngine;
using System.Collections;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T _instance;
	private static object _lock = new object();

	public static T Instance
	{
		get
		{
			if (applicationIsQuitting) {
				return null;
			}

			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType(typeof(T));

					if (_instance == null)
					{
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
						singleton.name = typeof(T).ToString();

						DontDestroyOnLoad(singleton);
					} 

					_instance.Init();
				}

				return _instance;
			}
		}
	}

	public static bool Exists { get { return _instance != null; } }

	private static bool applicationIsQuitting = false;

	public virtual void Init() { }

	protected virtual void OnDestroying() { }

	public void OnDestroy ()
	{
		OnDestroying();
		applicationIsQuitting = true;
	}
}
