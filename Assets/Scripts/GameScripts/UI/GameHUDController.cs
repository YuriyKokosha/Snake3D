using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EventsSystem;

public class GameHUDController 
	: MonoBehaviour
	, IHandleEvent<EventFPSValueChanged>
	, IHandleEvent<EventSpeedChanged>
{
	[Header("Texts")]
	[SerializeField]
	private Text _fpsText;

	[SerializeField]
	private Text _speedText;

	[Header("Buttons")]
	[SerializeField]
	private UIButtonPresenter _buttonLeft;

	[SerializeField]
	private UIButtonPresenter _buttonRight;

	[SerializeField]
	private UIButtonPresenter _buttonUp;

	[SerializeField]
	private UIButtonPresenter _buttonDown;

	[SerializeField]
	private UIButtonPresenter _buttonQuit;

	void OnEnable()
	{
		EventBus.Instance.Subscribe<EventFPSValueChanged>(this);
		EventBus.Instance.Subscribe<EventSpeedChanged>(this);

		_buttonLeft.OnButtonClicked += OnButtonClicked;
		_buttonRight.OnButtonClicked += OnButtonClicked;
		_buttonUp.OnButtonClicked += OnButtonClicked;
		_buttonDown.OnButtonClicked += OnButtonClicked;
		_buttonQuit.OnButtonClicked += OnButtonClicked;
	}

	void OnDisable()
	{
		_buttonLeft.OnButtonClicked -= OnButtonClicked;
		_buttonRight.OnButtonClicked -= OnButtonClicked;
		_buttonUp.OnButtonClicked -= OnButtonClicked;
		_buttonDown.OnButtonClicked -= OnButtonClicked;
		_buttonQuit.OnButtonClicked -= OnButtonClicked;

		if (EventBus.Exists)
		{
			EventBus.Instance.Unsubscribe<EventFPSValueChanged>(this);
			EventBus.Instance.Unsubscribe<EventSpeedChanged>(this);
		}
	}
		
	#region IHandleEvent implementation

	public void Handle(object sender, EventFPSValueChanged data)
	{
		OnFpsCounterUpdated(data.Value);
	}

	public void Handle(object sender, EventSpeedChanged data)
	{
		OnSpeedUpdated(data.Value);
	}

	#endregion

	private void OnButtonClicked(UIButtonType type)
	{
		EventBus.Instance.Post<EventOnButtonClicked>(this, new EventOnButtonClicked(type));
	}

	private void OnFpsCounterUpdated(float fps)
	{
		_fpsText.color = ((fps >= 30) ? Color.green : ((fps > 10) ? Color.yellow : Color.red));
		_fpsText.text = "FPS: " + fps.ToString();
	}

	private void OnSpeedUpdated(float speed)
	{
		_speedText.text = "Speed: " + speed.ToString();
	}
}
