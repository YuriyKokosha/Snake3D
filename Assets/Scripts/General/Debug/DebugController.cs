using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EventsSystem;

public class DebugController : MonoSingleton<DebugController>
{
	[SerializeField]
	private float _updateTime = 0.3f;

	private float _accum = 0f;
	private int _frames = 0;

	void Start()
	{
		StartCoroutine(UpdateFpsCounter());
	}

	void Update () 
	{
		_accum += Time.timeScale / Time.deltaTime;
		++_frames;
	}

	private IEnumerator UpdateFpsCounter()
	{
		while (true)
		{
			float fps = _accum/_frames;

			EventBus.Instance.Post<EventFPSValueChanged>(this, new EventFPSValueChanged(fps));

			_accum = 0f;
			_frames = 0;

			yield return new WaitForSeconds(_updateTime);
		}
	}
}
